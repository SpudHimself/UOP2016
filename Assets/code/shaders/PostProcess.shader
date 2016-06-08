Shader "Custom/PostProcess" {
	Properties {
		_FadePhase ( "Fade Amount", Range( 0, 1 ) ) = 0.0
	}
	SubShader {
		Pass {
			Tags { "RenderType"="Opaque" }
			LOD 200
		
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform float _FadePhase;

			float4 frag( appdata_full i ) : COLOR {
				float grey = ( i.color.r + i.color.g + i.color.b ) / 3;
				float4 greyScale = float4( grey, grey, grey, 1.0 );
				return greyScale * i.texcoord.rgba;
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
