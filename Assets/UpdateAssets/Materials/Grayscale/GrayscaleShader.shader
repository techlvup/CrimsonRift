Shader "Custom/GrayscaleShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Threshold ("Black Threshold", Range(0, 1)) = 0.2 // ���ƺ�ɫ�������ֵ
        _GrayFactor ("Gray Factor", Range(0, 1)) = 1.0 // ���ƻҶȻ���ǿ��
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        // ������Ⱦ����Ϊ "Overlay" ȷ��͸��ͼ������ͨͼ��֮����Ⱦ
        Tags { "Queue"="Overlay" }

        Pass
        {
            // ʹ��͸���Ȼ��
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            Lighting Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Threshold;      // ���ƺ�ɫ�������ֵ
            float _GrayFactor;     // ���ƻҶȻ���ǿ��

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);  // ת������λ�õ��ü��ռ�
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex); // ������������
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);

                if (col.a > 0.0)
                {
                    float gray = dot(col.rgb, half3(0.299, 0.587, 0.114));

                    if (gray < _Threshold)
                    {
                        col.rgb = gray.xxx * _GrayFactor;
                    }
                    else
                    {
                        col.rgb = gray.xxx * _GrayFactor;
                    }
                }

                return col;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}