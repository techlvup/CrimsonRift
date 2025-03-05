using UnityEngine;
using UnityEngine.UI;



public class UIMask : MonoBehaviour
{
    private Image[] m_image;
    private ParticleSystem[] m_particleSystem;
    private MeshRenderer[] m_meshRenderer;



    public void ShowMask()
    {
        m_image = GetComponentsInChildren<Image>();
        m_particleSystem = GetComponentsInChildren<ParticleSystem>();
        m_meshRenderer = GetComponentsInChildren<MeshRenderer>();

        string[] assetNames1 = new string[] { "UIMaskParentMaterial.mat" };

        AssetBundleManager.LoadAssetBundle(DataUtilityManager.m_localRootPath + "AssetBundles/" + DataUtilityManager.m_platform + "/materials/uimaskparent/uimaskparentmaterial.mat_ab", assetNames1, (name, asset) => {
            if (name == assetNames1[0])
            {
                Material material = asset as Material;
                m_image[0].material = material;
            }
        });

        string[] assetNames2 = new string[] { "UIMaskChildrenMaterial.mat" };

        AssetBundleManager.LoadAssetBundle(DataUtilityManager.m_localRootPath + "AssetBundles/" + DataUtilityManager.m_platform + "/materials/uimaskchildren/uimaskchildrenmaterial.mat_ab", assetNames2, (name, asset) => {
            if (name == assetNames2[0])
            {
                Material material = asset as Material;

                if(m_image.Length > 1)
                {
                    for (int i = 1; i < m_image.Length; i++)
                    {
                        m_image[i].material = material;
                    }
                }

                if (m_particleSystem.Length > 0)
                {
                    for (int i = 0; i < m_particleSystem.Length; i++)
                    {
                        m_particleSystem[i].GetComponent<ParticleSystemRenderer>().material = material;
                    }
                }

                if (m_meshRenderer.Length > 0)
                {
                    for (int i = 0; i < m_meshRenderer.Length; i++)
                    {
                        m_meshRenderer[i].material = material;
                    }
                }
            }
        });
    }
}