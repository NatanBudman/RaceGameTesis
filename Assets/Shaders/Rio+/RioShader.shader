// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "RioShader"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Frecenci("Frecenci", Range( 0 , 1.5)) = 1
		_Speed("Speed", Float) = 0.2
		_Movimiento("Movimiento", Range( 0 , 15)) = 8.549818
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform float _Frecenci;
		uniform float _Speed;
		uniform float _Movimiento;
		uniform sampler2D _TextureSample0;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float mulTime5 = _Time.y * _Speed;
			v.vertex.xyz += ( ase_vertexNormal * float3( sin( ( ( v.texcoord.xy * _Frecenci ) + mulTime5 ) ) ,  0.0 ) * _Movimiento );
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime5 = _Time.y * _Speed;
			float2 temp_cast_0 = (mulTime5).xx;
			float2 uv_TexCoord35 = i.uv_texcoord + ( i.vertexColor * sin( ( ( i.uv_texcoord.x * _Frecenci ) + mulTime5 ) ) ).rg;
			float2 panner36 = ( 1.0 * _Time.y * temp_cast_0 + uv_TexCoord35);
			o.Emission = tex2D( _TextureSample0, panner36 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
8;406;841;273;2132.552;-246.1325;1.815504;True;False
Node;AmplifyShaderEditor.RangedFloatNode;4;-1653.097,800.4254;Inherit;False;Property;_Speed;Speed;2;0;Create;True;0;0;0;False;0;False;0.2;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1996.549,402.2817;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;2;-2031.726,573.7877;Inherit;True;Property;_Frecenci;Frecenci;1;0;Create;True;0;0;0;False;0;False;1;20.00555;0;1.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;5;-1379.555,783.5823;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-1795.889,158.6087;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-1703.989,6.569368;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;33;-1489.598,12.3549;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;34;-1514.518,-233.1487;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-1679.912,558.7728;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1252.701,-167.5144;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-1190.913,557.8859;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;35;-1108.451,-72.12;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;36;-868.2402,-63.72478;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1349.708,394.2337;Inherit;False;Property;_Movimiento;Movimiento;3;0;Create;True;0;0;0;False;0;False;8.549818;0;0;15;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;8;-1016.993,562.6473;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalVertexDataNode;9;-1361.754,189.636;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-904.1448,299.9168;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;1;-697.2525,-0.2058814;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;db02e5d0b8c505a4e81d4eed0bda32fe;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,22.52098;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;RioShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;4;0
WireConnection;27;0;3;1
WireConnection;27;1;2;0
WireConnection;25;0;27;0
WireConnection;25;1;5;0
WireConnection;33;0;25;0
WireConnection;6;0;3;0
WireConnection;6;1;2;0
WireConnection;22;0;34;0
WireConnection;22;1;33;0
WireConnection;7;0;6;0
WireConnection;7;1;5;0
WireConnection;35;1;22;0
WireConnection;36;0;35;0
WireConnection;36;2;5;0
WireConnection;8;0;7;0
WireConnection;11;0;9;0
WireConnection;11;1;8;0
WireConnection;11;2;13;0
WireConnection;1;1;36;0
WireConnection;0;2;1;0
WireConnection;0;11;11;0
ASEEND*/
//CHKSM=085012C531D66B2168125ECA56DBC1EBD2043CB7