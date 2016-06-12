Shader "NewChromantics/KinectMergeShader" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_DepthTex ("_DepthTex", 2D) = "white" {}
		DepthRangeMax("DepthRangeMax", Range(0,1) ) = 0.15
		DepthRangeMin("DepthRangeMin", Range(0,1) ) = 0
		DiscardNonDepth("DiscardNonDepth", int ) = 0
		InvalidColour("InvalidColour", COLOR ) = (0,0,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			//#pragma multi_compile DEPTH_IS_RED DEPTH_IS_RED_GREEN
			#define DEPTH_IS_RED 0
			#define DEPTH_IS_RED_GREEN 1
			#define DEBUG_OUTPUT	0

			sampler2D _MainTex;
			sampler2D _DepthTex;
			float DepthRangeMax;		//	normalised
			float DepthRangeMin;		//	normalised
			float4 InvalidColour;
			int DiscardNonDepth;


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

				#if DEPTH_IS_RED
					float Depth = DepthRgba.x;
				#elif DEPTH_IS_RED_GREEN

					//	get normalised depth
					float Depth = (DepthRgba.x*255.f) + (DepthRgba.y * (255.f*255.f) );
					Depth /= 255.f + (255.f*255.f);
					
					//	check for invalid pixels
					//bool InvalidDepth = (DepthRgba.z > 0.5f);
					//if ( InvalidDepth )
					//	return -1;
						
					
				#endif

				if ( Depth > DepthRangeMax )
					return -1;
				if ( Depth < DepthRangeMin )
					return -1;

				//	normalise to our range
				Depth = (Depth-DepthRangeMin) / (DepthRangeMax-DepthRangeMin);
				return Depth;
			}

			fixed4 frag(FragInput In) : SV_Target {
				
				float Depth = GetDepth( In.uv_MainTex );
				float3 Colour = tex2D( _MainTex, In.uv_MainTex ).rgb;
				float4 Rgba = float4(0,0,0,1);

				if ( Depth < 0 )
				{	
					Rgba = InvalidColour;

					if ( DiscardNonDepth )
						discard;
				}
				else
				{
					if ( DEBUG_OUTPUT )
						return lerp( float4(1,0,0,1), float4(0,1,0,1), Depth );
				
					Rgba.xyz = Colour.xyz ;// * (1-Depth);
				}
				
				return Rgba;				
			}

		ENDCG
	}
	}

}
