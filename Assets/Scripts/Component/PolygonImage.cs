using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonImage : Image
{
    private PolygonCollider2D polyCollider;

    protected override void Awake()
    {
        base.Awake();
        polyCollider = gameObject.GetComponent<PolygonCollider2D>();
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        // ת��ΪUI�ֲ�����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out Vector2 localPoint);

        // ת��Ϊ��ײ������ϵ������������������ϵ��
        Vector2 pivotOffset = rectTransform.rect.size * rectTransform.pivot;
        localPoint += pivotOffset;

        // �����Ƿ��ڶ������ײ����
        return polyCollider.OverlapPoint(localPoint);
    }
}