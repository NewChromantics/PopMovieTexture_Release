Shader "NewChromantics/KinectMergeShader" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_DepthTex ("_DepthTex", 2D) = "white" {}
		DepthMax("DepthMax", Range(256,10000) ) = 10000
		DepthRangeMax("DepthRangeMax", Range(0,1) ) = 0.5
		DepthRangeMin("DepthRangeMin", Range(0,1) ) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			sampler2D _DepthTex;
			float DepthMax;
			float DepthRangeMax;		//	normalised
			float DepthRangeMin;		//	normalised
			
			struct VertexInput {
				float4 Position : POSITION;
				float2 uv_MainTex : TEXCOORD0;
			};
			
			struct FragInput {
				float4 Position : SV_POSITION;
				float2	uv_MainTex : TEXCOORD0;
			};

			
			FragInput vert(VertexInput In) {
				FragInput Out;
				Out.Position = mul (UNITY_MATRIX_MVP, In.Position );
				Out.uv_MainTex = In.uv_MainTex;
				return Out;
			}
			
			float GetDepth(float2 uv)
			{
				float4 DepthRgba = tex2D( _DepthTex, uv );
				float Depth = (DepthRgba.x*256.f) + (DepthRgba.y * (256.f*256.f) );
				
				//	invalid reading
				//if ( Depth == 0 )	return -1;
					
				//	input range
				Depth /= DepthMax;
				if ( Depth > DepthRangeMax )
					return -1;
				if ( Depth < DepthRangeMin )
					return -1;
	
				Depth = (Depth-DepthRangeMin) / (DepthRangeMax-DepthRangeMin);
				return Depth;
			}

			fixed4 frag(FragInput In) : SV_Target {
				
				float Depth = GetDepth( In.uv_MainTex );
				float3 Colour = tex2D( _MainTex, In.uv_MainTex ).rgb;
				float4 Rgba = float4(0,0,0,1);
			
				if ( Depth < 0 )
				{	
					Rgba = float4(1,0,0,0);
					discard;
				}
				else
				{		
					Rgba.xyz = Colour.xyz ;// * (1-Depth);
				}
				
				return Rgba;
				
			}

		ENDCG
	}
	}

}
