using UnityEngine;



[ExecuteAlways] // ֧�ֱ༭��ʵʱԤ��
public class ScreenAdapter : MonoBehaviour
{
    private RectTransform m_tsPanel;
    private DrivenRectTransformTracker m_tracker;



    private void OnEnable()
    {
        m_tsPanel = GetComponent<RectTransform>();
        AdjustScreen();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Application.isPlaying)
        {
            return;
        }

        AdjustScreen(); // �༭��������ʱʵʱ����
    }
#endif

    private void OnDisable() => m_tracker.Clear();



    private void AdjustScreen()
    {
        if (m_tsPanel == null)
        {
            return;
        }

        // ���������ԣ��Զ����£�
        m_tracker = new DrivenRectTransformTracker();
        m_tracker.Add(this, m_tsPanel, DrivenTransformProperties.AnchorMin | DrivenTransformProperties.AnchorMax);

        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        // ��һ�����ֵ꣨��0��1��
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        m_tsPanel.anchorMin = anchorMin;
        m_tsPanel.anchorMax = anchorMax;
    }
}