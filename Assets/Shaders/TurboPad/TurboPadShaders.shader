// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TurboPadShaders"
{
	Properties
	{
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 15
		_Color1("Color1", Color) = (0,0.05483294,1,0)
		_Color2("Color 2", Color) = (0,0.9717824,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 4.6
		struct Input
		{
			float3 worldNormal;
			float2 uv_texcoord;
		};

		uniform float4 _Color1;
		uniform float4 _Color2;
		uniform float _EdgeLength;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float3 ase_vertexNormal = v.normal.xyz;
			float mulTime91 = _Time.y * 8.0;
			float3 temp_output_83_0 = ( ase_vertexNormal * sin( ( ( -v.texcoord.xy.x * 12.35427 ) + mulTime91 ) ) * 0.03145531 );
			v.normal = temp_output_83_0;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_vertexNormal = mul( unity_WorldToObject, float4( ase_worldNormal, 0 ) );
			float mulTime91 = _Time.y * 8.0;
			float3 temp_output_83_0 = ( ase_vertexNormal * sin( ( ( -i.uv_texcoord.x * 12.35427 ) + mulTime91 ) ) * 0.03145531 );
			float3 temp_output_104_0 = step( float3( 0,0,0 ) , temp_output_83_0 );
			o.Albedo = ( ( float4( -temp_output_104_0 , 0.0 ) + _Color1 ) + ( float4( temp_output_104_0 , 0.0 ) + ( _Color2 * 1.854676 ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.6
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;398;1009;289;1797.215;428.8273;2.216631;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;85;-1082.917,-124.5192;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;90;-875.2595,128.9192;Inherit;False;Constant;_Speed;Speed;2;0;Create;True;0;0;0;False;0;False;8;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;121;-835.1968,-96.33266;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;89;-890.4183,26.17683;Inherit;False;Constant;_Frecenci;Frecenci;2;0;Create;True;0;0;0;False;0;False;12.35427;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;86;-604.0867,-111.9359;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;91;-600.7176,112.0762;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;87;-412.0758,-113.6202;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;118;-233.239,-71.38363;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;84;-732.0944,-373.0014;Inherit;False;Constant;_Amplitud;Amplitud;2;0;Create;True;0;0;0;False;0;False;0.03145531;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;82;-667.7386,-580.1815;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;115.631,-274.5464;Inherit;True;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;116;421.6461,248.2484;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;1.854676;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;96;250.1353,67.93983;Inherit;False;Property;_Color2;Color 2;6;0;Create;True;0;0;0;False;0;False;0,0.9717824,1,0;0,0.9717824,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;104;367.0727,-166.0753;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;115;549.1922,47.45813;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.NegateNode;112;600.621,-187.6248;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;101;91.73697,-504.3221;Inherit;False;Property;_Color1;Color1;5;0;Create;True;0;0;0;False;0;False;0,0.05483294,1,0;0,0.05483294,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;105;771.8497,-137.0836;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;109;773.5524,-495.4923;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;114;1147.311,-443.0134;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1387.047,-583.7427;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;TurboPadShaders;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;0;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;121;0;85;1
WireConnection;86;0;121;0
WireConnection;86;1;89;0
WireConnection;91;0;90;0
WireConnection;87;0;86;0
WireConnection;87;1;91;0
WireConnection;118;0;87;0
WireConnection;83;0;82;0
WireConnection;83;1;118;0
WireConnection;83;2;84;0
WireConnection;104;1;83;0
WireConnection;115;0;96;0
WireConnection;115;1;116;0
WireConnection;112;0;104;0
WireConnection;105;0;104;0
WireConnection;105;1;115;0
WireConnection;109;0;112;0
WireConnection;109;1;101;0
WireConnection;114;0;109;0
WireConnection;114;1;105;0
WireConnection;0;0;114;0
WireConnection;0;12;83;0
ASEEND*/
//CHKSM=5A46BB7D26BFFBD79CE672822B1FADC94DE1410A