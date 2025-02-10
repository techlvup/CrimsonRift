#pragma vertex analyzeInputData// ָ��������ɫ������
#pragma fragment getOutputData// ָ��Ƭ����ɫ������
#include "E:\UnityEditor\2022.3.51f1c1\Editor\Data\CGIncludes\UnityCG.cginc"

float4 _Color; // ��ɫ
float _Shininess; // �����
float _RangeValue; // ��Χֵ
float4 _Offset; // ����
sampler2D _MainTex; // ʵ�ʵ�2D�������
float4 _MainTex_ST; // ������������صı任��Ϣ��ƽ�ƺ����ţ�
samplerCUBE _TextureCube; // ����������
bool _EnableEffect; // ����ֵ

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
    return i.color;
}