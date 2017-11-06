// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "JellyCatch/SandShader" {
	Properties {
		_MainColor("Main Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Texture", 2D) = "white" {}
		_Bump ("Bump", 2D) = "bump" {}
		_Sand("Level of sand", Range(1, -1)) = 1
		_SandColor ("Color of sand", Color) = (1.0, 1.0, 1.0, 1.0)
		_SandDirection("Direction of sand", Vector) = (0,1,0)
		_SandDepth("Depth of Sand", Range(0, .001)) = 0
		_Scale("Scale of texture", Float) = 1.0
		_ElevationPoint("Starting height point for the floor", Float) = 0
		_ElevationRange("Range +/- from the ElevationPoint", Range(0, 2)) = 0 
	}
	SubShader {
		Tags {"RenderType" = "Opaque"}
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		sampler2D _Bump;
		float _Sand;
		float4 _SandColor;
		float4 _MainColor;
		float4 _SandDirection;
		float _SandDepth;
		float _Scale;
		float _ElevationPoint;
		float _ElevationRange;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Bump;
			float3 worldNormal;
			float3 worldPos;
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

		void vert(inout appdata_full v) {
			float4 sd = mul(_SandDirection, unity_WorldToObject);
			if (dot(v.normal, sd.xyz) >= _Sand)
				v.vertex.xyz += (sd.xyz + v.normal) * _SandDepth * _Sand;
		}
		
		void surf (Input IN, inout SurfaceOutput o) {
			//half4 c = tex2D (_MainTex, IN.uv_MainTex);
			half4 c = CalculateTriplanarColor(IN);
			o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump));
			float maxElevation = _ElevationPoint + _ElevationRange;
			float minElevation = _ElevationPoint - _ElevationRange;
			if(IN.worldPos[1] <= maxElevation && IN.worldPos[1] >= minElevation && dot(WorldNormalVector(IN, o.Normal), _SandDirection.xyz) >= _Sand)
				o.Albedo = c.rgb * _SandColor;
			else
				o.Albedo = _MainColor;
			
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
