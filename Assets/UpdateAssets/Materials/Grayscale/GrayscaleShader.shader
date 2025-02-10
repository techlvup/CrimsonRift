Shader "MyShader/Grayscale"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
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

            HLSLPROGRAM
            #pragma vertex analyzeInputData// ָ��������ɫ������
            #pragma fragment getOutputData// ָ��Ƭ����ɫ������
            #include "UnityCG.cginc"

            sampler2D _MainTex; // ʵ�ʵ�2D�������
            float4 _MainTex_ST; // ������������صı任��Ϣ��ƽ�ƺ����ţ�

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
                o.color = v.color;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // �Զ���_MainTex_ST������Ӧ�õ�uv������
                o.normal = v.normal; // ��ӷ��ߵĳ�ʼ��
                return o;
            }

            half4 getOutputData(fragmentStruct i) : SV_Target
            {
                half4 color = tex2D(_MainTex, i.uv);
                float gray = dot(color.rgb, half3(0.299, 0.587, 0.114));//�Ҷȹ�ʽ
                color.rgb = gray.xxx;//float3(gray, gray, gray)
                return color;
            }
            ENDHLSL
        }
    }

    Fallback "Diffuse"
}