// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ForceFieldShader"
{
	Properties
	{
		_Frecenci("Frecenci", Range( 0 , 25)) = 20.00555
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 15
		_Speed("Speed", Float) = 5
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_Transparencia("Transparencia", Range( 0 , 10)) = 1.078197
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_Color2("Color 2", Color) = (0,1,0.2654507,0)
		_Color1("Color 1", Color) = (1,0.9071339,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 4.6
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
		};

		uniform float _Frecenci;
		uniform float _Speed;
		uniform float4 _Color2;
		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;
		uniform float4 _Color1;
		uniform float _Transparencia;
		uniform sampler2D _TextureSample2;
		uniform float4 _TextureSample2_ST;
		uniform float _EdgeLength;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float3 ase_vertexNormal = v.normal.xyz;
			float mulTime28 = _Time.y * _Speed;
			float3 temp_output_32_0 = ( ase_vertexNormal * sin( ( ( v.texcoord.xy.y * _Frecenci ) + mulTime28 ) ) * 0.03145531 );
			float2 uv_TextureSample1 = v.texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			float4 tex2DNode45 = tex2Dlod( _TextureSample1, float4( uv_TextureSample1, 0, 0.0) );
			float4 temp_output_44_0 = ( ( ( float4( step( -temp_output_32_0 , float3( 0,0,0 ) ) , 0.0 ) + _Color2 ) + tex2DNode45 ) + ( tex2DNode45 + ( float4( step( float3( 0,0,0 ) , temp_output_32_0 ) , 0.0 ) + ( _Color1 * 0.7082514 ) ) ) );
			v.normal = temp_output_44_0.rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color79 = IsGammaSpace() ? float4(1,0,0,0) : float4(1,0,0,0);
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			float4 tex2DNode45 = tex2D( _TextureSample1, uv_TextureSample1 );
			float4 temp_output_64_0 = ( 1.0 - tex2DNode45 );
			float4 color53 = IsGammaSpace() ? float4(0.1736842,1,0,0) : float4(0.02548947,1,0,0);
			float2 uv_TextureSample2 = i.uv_texcoord * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_vertexNormal = mul( unity_WorldToObject, float4( ase_worldNormal, 0 ) );
			float mulTime28 = _Time.y * _Speed;
			float3 temp_output_32_0 = ( ase_vertexNormal * sin( ( ( i.uv_texcoord.y * _Frecenci ) + mulTime28 ) ) * 0.03145531 );
			float4 temp_output_44_0 = ( ( ( float4( step( -temp_output_32_0 , float3( 0,0,0 ) ) , 0.0 ) + _Color2 ) + tex2DNode45 ) + ( tex2DNode45 + ( float4( step( float3( 0,0,0 ) , temp_output_32_0 ) , 0.0 ) + ( _Color1 * 0.7082514 ) ) ) );
			float4 lerpResult67 = lerp( ( ( temp_output_64_0 + color53 ) + ( tex2DNode45 + ( color53 * _Transparencia ) ) + ( tex2DNode45.a + color53 ) ) , tex2D( _TextureSample2, uv_TextureSample2 ) , temp_output_44_0);
			o.Emission = ( ( color79 + step( temp_output_64_0 , float4( 0,0,0,0 ) ) ) + lerpResult67 ).rgb;
			o.Alpha = -tex2DNode45.a;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 

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
			sampler3D _DitherMaskLOD;
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
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
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
8;333;841;346;3016.34;-20.53065;2.499441;True;False
Node;AmplifyShaderEditor.RangedFloatNode;25;-2356.472,882.2359;Inherit;False;Property;_Speed;Speed;6;0;Create;True;0;0;0;False;0;False;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;26;-2649.489,605.6069;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;27;-2372.631,779.4935;Inherit;False;Property;_Frecenci;Frecenci;0;0;Create;True;0;0;0;False;0;False;20.00555;20.00555;0;25;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-2086.299,641.3808;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;28;-2082.93,865.3929;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-1894.288,639.6965;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;40;-2149.951,173.1352;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;31;-1725.858,648.118;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-2214.307,380.3153;Inherit;False;Constant;_Amplitud;Amplitud;2;0;Create;True;0;0;0;False;0;False;0.03145531;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-1654.923,450.9225;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NegateNode;37;-1478.514,360.2597;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-1506.539,987.5408;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;0.7082514;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;35;-1630.561,892.2135;Inherit;False;Property;_Color1;Color 1;11;0;Create;True;0;0;0;False;0;False;1,0.9071339,0,0;0,0.9717824,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;53;-658.8228,1191.031;Inherit;False;Constant;_Color0;Color 0;5;0;Create;True;0;0;0;False;0;False;0.1736842,1,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;41;-1443.045,119.2156;Inherit;False;Property;_Color2;Color 2;10;0;Create;True;0;0;0;False;0;False;0,1,0.2654507,0;0,0.05483294,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-1378.993,786.7505;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-331.1511,1407.694;Inherit;False;Property;_Transparencia;Transparencia;8;0;Create;True;0;0;0;False;0;False;1.078197;0.2266882;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;34;-1458.495,503.0953;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StepOpNode;56;-1245.737,367.1482;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;45;-1042.247,859.9556;Inherit;True;Property;_TextureSample1;Texture Sample 1;7;0;Create;True;0;0;0;False;0;False;-1;36f19ca6d70556c43a4c60bb6afd1879;36f19ca6d70556c43a4c60bb6afd1879;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-293.9248,1163.95;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;64;-595.2772,858.0999;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;38;-1105.897,582.5746;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;-1025.609,224.1661;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;52;-271.1133,954.4316;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;60;-373.6764,737.2411;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;65;-538.5337,1024.625;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;-647.2082,570.6977;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-591.7583,275.395;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;79;-655.2544,-15.38818;Inherit;False;Constant;_Color3;Color 3;8;0;Create;True;0;0;0;False;0;False;1,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;63;-120.2892,746.5982;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;44;-397.967,279.541;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;69;-457.5167,134.2741;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;66;-1046.591,1072.301;Inherit;True;Property;_TextureSample2;Texture Sample 2;9;0;Create;True;0;0;0;False;0;False;-1;37e6f91f3efb0954cbdce254638862ea;37e6f91f3efb0954cbdce254638862ea;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;67;-128.2743,498.7707;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;82;-253.9623,65.34966;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VoronoiNode;58;-1342.845,1120.031;Inherit;False;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;50;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.NegateNode;3;-18.36341,1018.165;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;81;58.4967,233.8472;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;270.5448,-23.53568;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;ForceFieldShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;29;0;26;2
WireConnection;29;1;27;0
WireConnection;28;0;25;0
WireConnection;30;0;29;0
WireConnection;30;1;28;0
WireConnection;31;0;30;0
WireConnection;32;0;40;0
WireConnection;32;1;31;0
WireConnection;32;2;39;0
WireConnection;37;0;32;0
WireConnection;36;0;35;0
WireConnection;36;1;33;0
WireConnection;34;1;32;0
WireConnection;56;0;37;0
WireConnection;54;0;53;0
WireConnection;54;1;55;0
WireConnection;64;0;45;0
WireConnection;38;0;34;0
WireConnection;38;1;36;0
WireConnection;42;0;56;0
WireConnection;42;1;41;0
WireConnection;52;0;45;0
WireConnection;52;1;54;0
WireConnection;60;0;64;0
WireConnection;60;1;53;0
WireConnection;65;0;45;4
WireConnection;65;1;53;0
WireConnection;47;0;45;0
WireConnection;47;1;38;0
WireConnection;43;0;42;0
WireConnection;43;1;45;0
WireConnection;63;0;60;0
WireConnection;63;1;52;0
WireConnection;63;2;65;0
WireConnection;44;0;43;0
WireConnection;44;1;47;0
WireConnection;69;0;64;0
WireConnection;67;0;63;0
WireConnection;67;1;66;0
WireConnection;67;2;44;0
WireConnection;82;0;79;0
WireConnection;82;1;69;0
WireConnection;3;0;45;4
WireConnection;81;0;82;0
WireConnection;81;1;67;0
WireConnection;0;2;81;0
WireConnection;0;9;3;0
WireConnection;0;12;44;0
ASEEND*/
//CHKSM=73D75D8A4CEC25E57334A0DC4D61B4655310C767