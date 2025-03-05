Shader "MyShader/UIMaskParent"
{
    Properties
    {
        [PerRendererData] _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Main Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
        }

        Pass
        {
            // ��Ⱦָ��
            Blend SrcAlpha OneMinusSrcAlpha

            ColorMask 0

            Stencil {
                Ref 1
                Comp Always
                Pass Replace
            }

            HLSLPROGRAM
            #pragma vertex analyzeInputData// ָ��������ɫ������
            #pragma fragment getOutputData// ָ��Ƭ����ɫ������
            #include "UnityCG.cginc"

            sampler2D _MainTex; // ʵ�ʵ�2D�������
            float4 _MainTex_ST; // ������������صı任��Ϣ��ƽ�ƺ����ţ�
            float4 _Color; // ��ɫ

            struct vertexStruct
            {
                float4 vertex : POSITION; // ����λ�ã�����ռ�����ռ䣩
                float3 normal : NORMAL; // ���㷨��
                float4 color : COLOR; // ������ɫ������еĻ���
                float2 uv : TEXCOORD0; // ��������
            };

            struct fragmentStruct
            {
                float4 pos : SV_POSITION; // �����ڲü��ռ��е�λ��
                float4 color : COLOR; // ������ɫ��������ֵ��
                float2 uv : TEXCOORD0; // �������꣨������ֵ��
                float3 normal : TEXCOORD1; // ���㷨�ߣ�������ֵ��
            };

            fragmentStruct analyzeInputData(vertexStruct v)
            {
                fragmentStruct o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color * _Color;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // �Զ���_MainTex_ST������Ӧ�õ�uv������
                o.normal = v.normal; // ��ӷ��ߵĳ�ʼ��
                return o;
            }

            half4 getOutputData(fragmentStruct i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv) * i.color;
                return color;
            }
            ENDHLSL
        }
    }

    Fallback "Diffuse"
}