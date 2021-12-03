// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "GameX/Hero_X" {

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
		LOD 400
		Pass
		{
			Name "XRAYH"	//xray quality high
			Tags{ "LightMode" = "Vertex" }
			Blend One OneMinusSrcColor
			//Cull Off
			Lighting Off
			ZWrite Off
			Ztest Greater

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest	

			fixed4 _Color;
			struct appdata{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			struct v2f{
				float4 vertex : SV_POSITION;
				fixed4 color: COLOR;
			};
			v2f vert(appdata v){
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				fixed3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				fixed  nl = saturate(1.0 - dot(v.normal, viewDir));	
				o.color = nl*nl;
				return o;
			}
			fixed4 frag(v2f i) : COLOR{
				//fixed4 col =  _AfterColor * _Color.a;
				//return col;
				return i.color* fixed4(0.5,0.5,0.3,1);
			}
			ENDCG
		}

		UsePass "GameX/Hero_Base/HQH"
	}

	SubShader
	{	
		LOD 300
		Pass
		{
			Name "XRAYM"	//xray quality middle
			Tags{ "LightMode" = "Vertex" }    
			Blend One OneMinusSrcColor
			Lighting Off
			ZWrite Off
			Ztest Greater
			
			CGPROGRAM
			#pragma vertex vert    
			#pragma fragment frag 
			#include "UnityCG.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest  
			
			struct appdata
			{
				float4 vertex : POSITION;							
			};
			struct v2f {				
				fixed4 pos : SV_POSITION;					
			};
			
			v2f vert (appdata v)
			{                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}
			fixed4 frag(v2f i) : COLOR
			{
				return fixed4(0.5,0.5,0.3,1);					
			}
			ENDCG
		}

		UsePass "GameX/Hero_Base/HQM"
	}

	SubShader
	{
		LOD 200
		// Pass
		// {
		// 	Name "XRAYL"	//xray quality low
		// 	Tags{ "LightMode" = "Vertex" }    
		// 	Blend One OneMinusSrcColor
		// 	Lighting Off
		// 	ZWrite Off
		// 	Ztest Greater
			
		// 	CGPROGRAM
		// 	#pragma vertex vert    
		// 	#pragma fragment frag 
		// 	#include "UnityCG.cginc"
		// 	#pragma fragmentoption ARB_precision_hint_fastest  
			
		// 	struct appdata
		// 	{
		// 		float4 vertex : POSITION;							
		// 	};
		// 	struct v2f {				
		// 		fixed4 pos : SV_POSITION;					
		// 	};
			
		// 	v2f vert (appdata v)
		// 	{                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
		// 		v2f o;
		// 		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		// 		return o;
		// 	}
		// 	fixed4 frag(v2f i) : COLOR
		// 	{
		// 		return fixed4(0.2,0.5,1,0.4);					
		// 	}
		// 	ENDCG
		// }

		UsePass "GameX/Hero_Base/HQL"
	}

	FallBack "Diffuse"
}