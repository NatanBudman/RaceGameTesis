// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/FlagPost"
{
	Properties
	{
		_Frecenci1("Frecenci", Range( 0 , 25)) = 25
		_Speed1("Speed", Float) = 8
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _Frecenci1;
		uniform float _Speed1;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float mulTime4 = _Time.y * _Speed1;
			v.vertex.xyz += ( ase_vertexNormal * sin( ( ( v.texcoord.xy.x * _Frecenci1 ) + mulTime4 ) ) * 0.5529412 );
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			o.Emission = tex2D( _TextureSample0, uv_TextureSample0 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;589;1009;98;1503.913;66.34223;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;1;-1225.767,412.4055;Inherit;False;Property;_Frecenci1;Frecenci;0;0;Create;True;0;0;0;False;0;False;25;20.00555;0;25;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-1502.624,238.5188;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-1209.606,515.1479;Inherit;False;Property;_Speed1;Speed;1;0;Create;True;0;0;0;False;0;False;8;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;4;-936.0645,498.3049;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-939.4346,274.2927;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-747.4226,272.6084;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;7;-1003.085,-193.9528;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-1067.442,13.22726;Inherit;False;Constant;_Amplitud1;Amplitud;2;0;Create;True;0;0;0;False;0;False;0.5529412;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;9;-578.9927,281.0299;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-508.0576,83.83449;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;12;-564.5388,-183.2159;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;0;False;0;False;-1;ae70a90b485be7b4cab8ef3a706b7237;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom/FlagPost;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;3;0
WireConnection;5;0;2;1
WireConnection;5;1;1;0
WireConnection;6;0;5;0
WireConnection;6;1;4;0
WireConnection;9;0;6;0
WireConnection;10;0;7;0
WireConnection;10;1;9;0
WireConnection;10;2;8;0
WireConnection;0;2;12;0
WireConnection;0;11;10;0
ASEEND*/
//CHKSM=2FE3593E4A4711646FD6879D0680C124D2094A60