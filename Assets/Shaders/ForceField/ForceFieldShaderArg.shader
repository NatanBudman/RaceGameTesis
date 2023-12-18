// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ForceFieldShaderArg"
{
	Properties
	{
		_Frecenci("Frecenci", Range( 0 , 25)) = 20.00555
		_Speed("Speed", Float) = 5
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_Transparencia("Transparencia", Range( 0 , 10)) = 1.7561
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_Color2("Color 2", Color) = (0,0.1634262,1,0)
		_Color0("Color 0", Color) = (1,0.9071339,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
		};

		uniform float _Frecenci;
		uniform float _Speed;
		uniform float4 _Color2;
		uniform sampler2D _TextureSample1;
		uniform float4 _Color0;
		uniform float _Transparencia;
		uniform sampler2D _TextureSample2;
		uniform float4 _TextureSample2_ST;


		float2 voronoihash39( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi39( float2 v, float time, inout float2 id, inout float2 mr, float smoothness )
		{
			float2 n = floor( v );
			float2 f = frac( v );
			float F1 = 8.0;
			float F2 = 8.0; float2 mg = 0;
			for ( int j = -1; j <= 1; j++ )
			{
				for ( int i = -1; i <= 1; i++ )
			 	{
			 		float2 g = float2( i, j );
			 		float2 o = voronoihash39( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
					float d = 0.5 * dot( r, r );
			 		if( d<F1 ) {
			 			F2 = F1;
			 			F1 = d; mg = g; mr = r; id = o;
			 		} else if( d<F2 ) {
			 			F2 = d;
			 		}
			 	}
			}
			return F1;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float mulTime4 = _Time.y * _Speed;
			float3 temp_output_10_0 = ( ase_vertexNormal * sin( ( ( v.texcoord.xy.y * _Frecenci ) + mulTime4 ) ) * 0.03145531 );
			float time39 = 0.0;
			float2 coords39 = v.texcoord.xy * 50.0;
			float2 id39 = 0;
			float2 uv39 = 0;
			float voroi39 = voronoi39( coords39, time39, id39, uv39, 0 );
			float2 temp_cast_1 = (voroi39).xx;
			float4 tex2DNode14 = tex2Dlod( _TextureSample1, float4( temp_cast_1, 0, 0.0) );
			float4 temp_output_34_0 = ( ( ( float4( step( -temp_output_10_0 , float3( 0,0,0 ) ) , 0.0 ) + _Color2 ) + tex2DNode14 ) + ( tex2DNode14 + ( float4( step( float3( 0,0,0 ) , temp_output_10_0 ) , 0.0 ) + ( _Color0 * 4.14687 ) ) ) );
			v.normal = temp_output_34_0.rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color31 = IsGammaSpace() ? float4(0.04245281,0.8382424,1,0) : float4(0.003290793,0.670688,1,0);
			float time39 = 0.0;
			float2 coords39 = i.uv_texcoord * 50.0;
			float2 id39 = 0;
			float2 uv39 = 0;
			float voroi39 = voronoi39( coords39, time39, id39, uv39, 0 );
			float2 temp_cast_0 = (voroi39).xx;
			float4 tex2DNode14 = tex2D( _TextureSample1, temp_cast_0 );
			float4 temp_output_22_0 = ( 1.0 - tex2DNode14 );
			float4 color18 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
			float2 uv_TextureSample2 = i.uv_texcoord * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_vertexNormal = mul( unity_WorldToObject, float4( ase_worldNormal, 0 ) );
			float mulTime4 = _Time.y * _Speed;
			float3 temp_output_10_0 = ( ase_vertexNormal * sin( ( ( i.uv_texcoord.y * _Frecenci ) + mulTime4 ) ) * 0.03145531 );
			float4 temp_output_34_0 = ( ( ( float4( step( -temp_output_10_0 , float3( 0,0,0 ) ) , 0.0 ) + _Color2 ) + tex2DNode14 ) + ( tex2DNode14 + ( float4( step( float3( 0,0,0 ) , temp_output_10_0 ) , 0.0 ) + ( _Color0 * 4.14687 ) ) ) );
			float4 lerpResult35 = lerp( ( ( temp_output_22_0 + color18 ) + ( tex2DNode14 + ( color18 * _Transparencia ) ) + ( tex2DNode14.a + color18 ) ) , tex2D( _TextureSample2, uv_TextureSample2 ) , temp_output_34_0);
			o.Emission = ( ( color31 + step( temp_output_22_0 , float4( 0,0,0,0 ) ) ) + lerpResult35 ).rgb;
			o.Alpha = -tex2DNode14.a;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
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
				vertexDataFunc( v, customInputData );
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
7;386;851;309;1965.192;472.2272;1.777036;True;False
Node;AmplifyShaderEditor.RangedFloatNode;2;-2906.571,440.9166;Inherit;False;Property;_Frecenci;Frecenci;0;0;Create;True;0;0;0;False;0;False;20.00555;20.00555;0;25;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-3183.428,267.0299;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-2890.411,543.659;Inherit;False;Property;_Speed;Speed;1;0;Create;True;0;0;0;False;0;False;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;4;-2616.869,526.816;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-2620.239,302.8038;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-2428.227,301.1195;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;7;-2683.89,-165.4417;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-2748.246,41.73837;Inherit;False;Constant;_Amplitud;Amplitud;2;0;Create;True;0;0;0;False;0;False;0.03145531;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;9;-2259.797,309.541;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-2188.862,112.3456;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.VoronoiNode;39;-1871.487,781.454;Inherit;False;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;50;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.ColorNode;13;-2211.99,468.6552;Inherit;False;Property;_Color0;Color 0;6;0;Create;True;0;0;0;False;0;False;1,0.9071339,0,0;0,0.9717824,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NegateNode;11;-2012.453,21.68277;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-2040.478,648.9638;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;4.14687;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;20;-1992.434,164.5184;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;18;-1192.762,852.454;Inherit;False;Constant;_Color1;Color 1;5;0;Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;15;-1779.676,28.57125;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;14;-1576.186,521.3787;Inherit;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;0;False;0;False;-1;36f19ca6d70556c43a4c60bb6afd1879;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-865.0901,1069.117;Inherit;False;Property;_Transparencia;Transparencia;3;0;Create;True;0;0;0;False;0;False;1.7561;0.2266882;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;19;-1976.984,-219.3613;Inherit;False;Property;_Color2;Color 2;5;0;Create;True;0;0;0;False;0;False;0,0.1634262,1,0;0,0.05483294,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1912.932,448.1736;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-1559.548,-114.4108;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;22;-1141.01,457.186;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-1639.836,243.9976;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-827.8638,825.373;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-1125.697,-63.18196;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;-907.6154,398.6641;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-805.0523,615.8547;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;28;-1181.147,232.1207;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-1072.473,686.048;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-654.2282,408.0212;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-931.9061,-59.03596;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;30;-991.4557,-204.3028;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;32;-1580.53,733.7241;Inherit;True;Property;_TextureSample2;Texture Sample 2;4;0;Create;True;0;0;0;False;0;False;-1;37e6f91f3efb0954cbdce254638862ea;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;31;-1189.193,-353.9651;Inherit;False;Constant;_Color3;Color 3;8;0;Create;True;0;0;0;False;0;False;0.04245281,0.8382424,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;35;-662.2133,160.1937;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-787.9013,-273.2273;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.NegateNode;38;-552.3024,679.588;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-475.4424,-104.7297;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;ForceFieldShaderArg;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;3;0
WireConnection;5;0;1;2
WireConnection;5;1;2;0
WireConnection;6;0;5;0
WireConnection;6;1;4;0
WireConnection;9;0;6;0
WireConnection;10;0;7;0
WireConnection;10;1;9;0
WireConnection;10;2;8;0
WireConnection;11;0;10;0
WireConnection;20;1;10;0
WireConnection;15;0;11;0
WireConnection;14;1;39;0
WireConnection;17;0;13;0
WireConnection;17;1;12;0
WireConnection;23;0;15;0
WireConnection;23;1;19;0
WireConnection;22;0;14;0
WireConnection;21;0;20;0
WireConnection;21;1;17;0
WireConnection;24;0;18;0
WireConnection;24;1;16;0
WireConnection;26;0;23;0
WireConnection;26;1;14;0
WireConnection;27;0;22;0
WireConnection;27;1;18;0
WireConnection;29;0;14;0
WireConnection;29;1;24;0
WireConnection;28;0;14;0
WireConnection;28;1;21;0
WireConnection;25;0;14;4
WireConnection;25;1;18;0
WireConnection;33;0;27;0
WireConnection;33;1;29;0
WireConnection;33;2;25;0
WireConnection;34;0;26;0
WireConnection;34;1;28;0
WireConnection;30;0;22;0
WireConnection;35;0;33;0
WireConnection;35;1;32;0
WireConnection;35;2;34;0
WireConnection;36;0;31;0
WireConnection;36;1;30;0
WireConnection;38;0;14;4
WireConnection;37;0;36;0
WireConnection;37;1;35;0
WireConnection;0;2;37;0
WireConnection;0;9;38;0
WireConnection;0;12;34;0
ASEEND*/
//CHKSM=2659F70241C3CAC2CAEF9B75057A0B597F9BFEBF