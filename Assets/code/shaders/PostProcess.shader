Shader "Custom/PostProcess" {
	Properties {
		_MainTex ( "Base (RGB)", 2D ) = "white" {}
		_FadePhase ( "Fade Amount", Range( 0, 1 ) ) = 0.0
	}
	SubShader {
		Pass {
			ZTest Always

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _FadePhase;

			//struct v2f {
			//	float4 position : SV_POSITION;
			//	float4 color : COLOR;
			//	float2 texcoord : TEXCOORD0;
			//};

			//v2f vert( appdata_full i ) {
			//	v2f o;
			//
			//	o.position = mul( i.vertex, UNITY_MATRIX_MVP );
			//	//o.color = i.color;
			//	o.texcoord = i.texcoord;
			//
			//	return o;
			//}

			float4 frag( v2f_img i ) : COLOR {
				float4 texel = tex2D( _MainTex, i.uv );

				float grey = ( texel.r * 0.299 + texel.g * 0.587 + texel.b * 0.114 );
				float3 greyScale = float3( grey, grey, grey );

				float4 lum = texel;
				lum.rgb = lerp( texel.rgb, greyScale, _FadePhase );
				return lum;
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
