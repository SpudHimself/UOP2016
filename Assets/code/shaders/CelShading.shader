Shader "Custom/CelShading" 
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader 
	{
		Tags 
		{
			"RenderType" = "Opaque"
		}
		LOD 200

		CGPROGRAM
		#pragma surface surf CelShading
		#pragma target 3.0

		half4 LightingCelShading(SurfaceOutput surface, half3 lightDirection, half attenuation) 
		{
			half eyePos = dot(surface.Normal, lightDirection);

			if (eyePos <= 0.0) 
			{
				eyePos = 0.0;
			}
			else
			{
				eyePos = 1.0;
			}

			half4 color;
			color.rgb = surface.Albedo * _LightColor0.rgb * (eyePos * attenuation * 2);
			color.a = surface.Alpha;

			return color;
		}

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input 
		{
			float2 texCoords;
		};

		void surf(Input input, inout SurfaceOutput output) 
		{
			fixed4 color = tex2D(_MainTex, input.texCoords) * _Color;

			output.Albedo = color.rgb;
			output.Alpha  = color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}