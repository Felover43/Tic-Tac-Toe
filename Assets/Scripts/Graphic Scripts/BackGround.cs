
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    Image m_image = null;


    private void Awake()
    {
        m_image = GetComponent<Image>();
        if(Settings.instance != null)
            m_image.sprite = Settings.instance.graphicSelector.BackGround;
        
    }


    public void SetBG(Sprite bg)
    {
        m_image.sprite = bg;
    }
}
