
Shader "JellyCatch/TriPlanarTexShader" {
	Properties {
		_MainColor("Main Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Texture", 2D) = "white" {}
		_Scale ("Scale", Float) = 1.0
	}
	SubShader {
		Tags {"RenderType" = "Opaque"}
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float4 _MainColor;
		float _Scale;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
		};

		half4 CalculateTriplanarColor(Input IN) {
			float3 blending = abs(IN.worldNormal);
			blending = normalize(max(blending, 0.00001));
			float b = (blending.x + blending.y + blending.z);
			blending /= float3(b, b, b);

			half4 xaxis = tex2D(_MainTex, IN.worldPos.yz * _Scale);
			half4 yaxis = tex2D(_MainTex, IN.worldPos.xz * _Scale);
			half4 zaxis = tex2D(_MainTex, IN.worldPos.xy * _Scale);

			half4 finalColor = xaxis * blending.x + yaxis * blending.y + zaxis * blending.z;
			return finalColor;
		}

		
		void surf (Input IN, inout SurfaceOutput o) {
			
			half4 finalColor = CalculateTriplanarColor(IN);
			o.Albedo = finalColor * _MainColor;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
