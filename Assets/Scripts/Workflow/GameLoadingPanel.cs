using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadingPanel : MonoBehaviour
{
    private Slider m_sliProgress;
    private Text m_textDes;
    private Text m_textProgress;



    private void Awake()
    {
        m_sliProgress = gameObject.transform.Find("Sli_Progress").GetComponent<Slider>();
        m_textDes = gameObject.transform.Find("Text_Des").GetComponent<Text>();
        m_textProgress = gameObject.transform.Find("Text_Progress").GetComponent<Text>();
    }



    public void SetProgress(float nowDownloadNum, float needDownloadNum)
    {
        m_sliProgress.value = nowDownloadNum / needDownloadNum;
        m_textProgress.text = nowDownloadNum + "/" + needDownloadNum;
    }

    public void SetProgress(Dictionary<string, long> currSizeList, long needDownloadSize)
    {
        long currSize = GetCurrDownloadSize(currSizeList);
        m_sliProgress.value = (currSize * 1.00f) / (needDownloadSize * 1.00f);
        m_textProgress.text = LuaCallCS.FormatFileByteSize(currSize) + "/" + LuaCallCS.FormatFileByteSize(needDownloadSize);
    }

    public long GetCurrDownloadSize(Dictionary<string, long> currSizeList)
    {
        long currSize = 0;

        foreach (var item in currSizeList)
        {
            currSize += item.Value;
        }

        return currSize;
    }

    public void SetDes(string text)
    {
        m_textDes.text = text;
    }
}