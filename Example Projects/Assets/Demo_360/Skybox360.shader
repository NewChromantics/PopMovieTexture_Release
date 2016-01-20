Shader "Skybox/Gradient Skybox"
{
    Properties
    {
    	_MainTex ("Texture", 2D) = "white" {}
    	
    	CameraFovHorz("CameraFovHorz",Range(0,360)) = 360
    	CameraFovTop("CameraFovTop", Range(0,360) ) = 0
    	CameraFovBottom("CameraFovBottom", Range(0,360) ) = 360
    	
        // The properties below are used in the custom inspector.
        _UpVectorPitch ("Up Vector Pitch", float) = 0
        _UpVectorYaw ("Up Vector Yaw", float) = 0
    }

    CGINCLUDE

    #include "UnityCG.cginc"
#define PIf	3.1415926535897932384626433832795f

    struct appdata
    {
        float4 position : POSITION;
        float3 texcoord : TEXCOORD0;
    };
    
    struct v2f
    {
        float4 position : SV_POSITION;
        float3 texcoord : TEXCOORD0;
    };
    
    sampler2D _MainTex;
    float CameraFovHorz;
    float CameraFovTop;
	float CameraFovBottom;
	 
    v2f vert (appdata v)
    {
        v2f o;
        o.position = mul (UNITY_MATRIX_MVP, v.position);
        o.texcoord = v.texcoord;
        return o;
    }
    
    
			float RadToDeg(float Rad)
			{
				return Rad * (360.0f / (PIf * 2.0f) );
			}
			float DegToRad(float Deg)
			{
				return Deg * ((PIf * 2.0f)/360.0f );
			}
			 
			 
    float Range(float Min,float Max,float Time)
			{
				return (Time-Min) / (Max-Min);
			}
			
			//	y = visible
			float2 RescaleFov(float Radians,float MinDegrees,float MaxDegrees)
			{
				float Deg = RadToDeg( Radians );
				Deg += 180;
				
				//	re-scale
				Deg = Range( MinDegrees, MaxDegrees, Deg );
				bool Visible = Deg >= 0 && Deg <= 1;
				Deg *= 360;
				
				Deg -= 180;
				Radians = DegToRad( Deg );
				
				return float2( Radians, Visible?1:0 );
			}
    
    float3 ViewToLatLonVisible(float3 View3)
			{
				View3 = normalize(View3);
				//	http://en.wikipedia.org/wiki/N-vector#Converting_n-vector_to_latitude.2Flongitude
				float x = View3.x;
				float y = View3.y;
				float z = View3.z;
	
			//	auto lat = tan2( x, z );
				float lat = atan2( x, z );
				
				//	normalise y
				float xz = sqrt( (x*x) + (z*z) );
				float normy = y / sqrt( (y*y) + (xz*xz) );
				float lon = sin( normy );
				//float lon = atan2( y, xz );
				
				//	stretch longitude...
				//	gr: removed this as UV was wrapping around
				//lon *= 2.0;
				//	gr: sin output is -1 to 1...
				lon *= PIf;
				
				bool Visible = true;
				float2 NewLat = RescaleFov( lat, 0, CameraFovHorz );
				float2 NewLon = RescaleFov( lon, CameraFovTop, CameraFovBottom );
				lat = NewLat.x;
				lon = NewLon.x;
				Visible = (NewLat.y*NewLon.y) > 0;
				
	
				return float3( lat, lon, Visible ? 1:0 );
			}
			
			float2 LatLonToUv(float2 LatLon,float Width,float Height)
			{
				//	-pi...pi -> -1...1
				float lat = LatLon.x;
				float lon = LatLon.y;
				
				lat /= PIf;
				lon /= PIf;
				
				//	-1..1 -> 0..2
				lat += 1.0;
				lon += 1.0;
				
				//	0..2 -> 0..1
				lat /= 2.0;
				lon /= 2.0;
								
				lat *= Width;
				lon *= Height;
				
				return float2( lat, lon );
			}
			//	3D view normal to equirect's UV. Z is 0 if invalid
			float3 ViewToUvVisible(float3 ViewDir)
			{
				float3 latlonVisible = ViewToLatLonVisible( ViewDir );
				latlonVisible.xy = LatLonToUv( latlonVisible.xy, 1, 1 );
				return latlonVisible;
			}
			
    
    float2 ViewToUv(float3 View3)
    {
    	float3 UvVisible = ViewToUvVisible( View3 );
    	return UvVisible.xy;
    }
    
    fixed4 frag (v2f i) : COLOR
    {
    	float2 uv = ViewToUv(i.texcoord);
    	
    	if ( uv.x < 0 && uv.y < 0 )
    		return float4(1,0,0,1);
    	if ( uv.x < 0 )
    		return float4(0,1,0,1);
    	if ( uv.y < 0 )
    		return float4(0,0,1,1);
    		
    	return tex2D( _MainTex, uv );
    /*
        half d = dot (normalize (i.texcoord), _UpVector) * 0.5f + 0.5f;
        return lerp (_Color1, _Color2, pow (d, _Exponent)) * _Intensity;
    */
    }

    ENDCG

    SubShader
    {
        Tags { "RenderType"="Background" "Queue"="Background" }
        Pass
        {
            ZWrite Off
            Cull Off
            Fog { Mode Off }
            CGPROGRAM
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
   // CustomEditor "GradientSkyboxInspector"
}