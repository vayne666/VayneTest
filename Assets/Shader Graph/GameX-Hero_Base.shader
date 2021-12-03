// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "GameX/Hero_Base" {

	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_ReflectTex("Reflect", 2D) = "black" {}
		_NormalTex ("Bumpmap (RGB)", 2D) = "bump" {}
		_sepc_tex("MaskTex(RGB)",2D)="white" {}
		_ReflectFactor("Reflect Factor", Range(0.08, 2)) = 0.08
		_ReflectPower("Reflect Power", Range(1, 4)) = 1
		_Rim("Rim",Range(0.08,1))=0.08		
	}

	SubShader
	{
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////		
		Pass
		{
			Name "HQH"	//hero quality high    3图: 法线贴图+mask贴图+高光反射贴图
			Tags{
				"RenderType" = "Transparent"
				"Queue" = "Transparent" 
				"LightMode"="Vertex"
			}
			Blend SrcAlpha OneMinusSrcAlpha
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag     
			#include "UnityCG.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest
			struct Input
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f 
			{
				float4  vertex : SV_POSITION;
				float2  uv : TEXCOORD0;
				float3	TtoV0 : TEXCOORD1;
				float3	TtoV1 : TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			sampler2D _ReflectTex;
			fixed _ReflectFactor;
			fixed _ReflectPower;
			sampler2D _NormalTex; float4 _NormalTex_ST;
			sampler2D _sepc_tex;
			fixed _Rim;
			
			v2f vert (appdata_tan v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				
				TANGENT_SPACE_ROTATION;
				o.TtoV0 = mul(rotation, UNITY_MATRIX_IT_MV[0].xyz);
				o.TtoV1 = mul(rotation, UNITY_MATRIX_IT_MV[1].xyz);
				return o;
				
			}

			fixed4 frag(v2f i) : COLOR
			{
				float4 c;
				half4 albedo = tex2D(_MainTex, i.uv);
				float3 normal = UnpackNormal(tex2D(_NormalTex, i.uv));
				float2 vn;
				vn.x = dot(i.TtoV0, normal);
				vn.y = dot(i.TtoV1, normal);
				float4 ref = tex2D(_ReflectTex, vn*0.5 + 0.5);
				fixed4 mask_1 = tex2D(_sepc_tex,i.uv);
				// 高光1和边缘光2
				fixed3  sepc01 = pow(ref.rgb, _ReflectPower)*mask_1.rgb *_ReflectFactor;
				fixed3  sepc02 = pow(ref.a,   _ReflectPower)*_Rim ;
				c.rgb = 1.0*albedo.rgb  + saturate(sepc01 + sepc02*_Color.rgb);
				c.a = albedo.a * _Color.a;

				return c;
			}
			ENDCG
		}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		Pass
		{
			Name "HQM"	//hero quality middle       2图: mask贴图+高光反射贴图 (无法线贴图)
			Tags{
				"RenderType" = "Transparent"
				"Queue" = "Transparent" 
				"LightMode"="Vertex"
			}
			Blend SrcAlpha OneMinusSrcAlpha
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag     
			#include "UnityCG.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest
			struct Input
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f 
			{
				float4  vertex : SV_POSITION;
				float2  uv : TEXCOORD0;

				float2  cap : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			sampler2D _ReflectTex;
			fixed _ReflectFactor;
			fixed _ReflectPower;
			sampler2D _NormalTex; float4 _NormalTex_ST;
			sampler2D _sepc_tex;
			fixed _Rim;
			
			v2f vert (appdata_tan v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				half2 capCoord;
				capCoord.x = dot(UNITY_MATRIX_IT_MV[0].xyz,v.normal);
				capCoord.y = dot(UNITY_MATRIX_IT_MV[1].xyz,v.normal);
				o.cap = capCoord * 0.5 + 0.5;
				return o;
				
			}

			fixed4 frag(v2f i) : COLOR
			{
				float4 c;
				half4 albedo = tex2D(_MainTex, i.uv);
				float4 ref = tex2D(_ReflectTex, i.cap);
				fixed4 mask_1 = tex2D(_sepc_tex,i.uv);
				// 高光1和边缘光2
				fixed3  sepc01 = pow(ref.rgb, _ReflectPower)*mask_1.rgb *_ReflectFactor;
				fixed3  sepc02 = pow(ref.a,   _ReflectPower)*_Rim ;
				c.rgb = 1.0*albedo.rgb  + saturate(sepc01 + sepc02*_Color.rgb);
				c.a = albedo.a * _Color.a;

				return c;
			}
			ENDCG
		}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		Pass
		{
			Name "HQL"	//hero quality low   1图: 高光反射贴图  (无mask图,无法线贴图)
			Tags{
				"LightMode"="Vertex"
			}
			Blend SrcAlpha OneMinusSrcAlpha
			Lighting Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag     
			#include "UnityCG.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest
			
			struct Input
			{
				half4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				
			};

			struct v2f 
			{
				half4  pos : SV_POSITION;
				half2  uv0 : TEXCOORD0;	
				float2  cap : TEXCOORD1;				
			};
			sampler2D _MainTex;
			fixed4 _Color;
			sampler2D _ReflectTex;
			fixed _Rim;
			fixed _ReflectPower;
			v2f vert(appdata_tan v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv0 = v.texcoord;   
				half2 capCoord;
				capCoord.x = dot(UNITY_MATRIX_IT_MV[0].xyz,v.normal);
				capCoord.y = dot(UNITY_MATRIX_IT_MV[1].xyz,v.normal);
				o.cap = capCoord * 0.5 + 0.5;
				return o;      
			}
			fixed4 frag(v2f i)  : COLOR
			{
				fixed4 albedo = tex2D(_MainTex, i.uv0);
				half4 ref = tex2D(_ReflectTex, i.cap);
				fixed3  sepc02 = pow(ref.a,   _ReflectPower)*_Rim ;
				return fixed4(albedo.rgb+sepc02,albedo.a*_Color.a);
			}
			ENDCG
		}
	}
}