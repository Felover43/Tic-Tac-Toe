
using UnityEngine;
/*
 Settings is made with Singleton Design
as I found it to be a nice way to transfer
and use importaint data through out the game but particularly
between scenes. I alows the music and and graphic to transition 
seemlessly.
Although its I bet that there are more efficent ways to implement this
data between scenes I wanted to show it regardless and it was nice to make.
 */

public class Settings : MonoBehaviour
{
    public static Settings instance;
    
    Gamemode m_gamemode;
    
    [SerializeField]
    Sprite m_X;
    [SerializeField]
    Sprite m_O;
    [SerializeField]
    Sprite m_BG;
    public GraphicSelector graphicSelector;
    public AudioSource music;

    void Awake()
    {
       if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        SetDefault();
        SetAudio();
    }

    void SetDefault()
    {
        m_gamemode = Gamemode.PvP;

        graphicSelector = gameObject.AddComponent<GraphicSelector>();
        graphicSelector.SetGraphics(m_X,m_O,m_BG );

    }
    void SetAudio()
    {
       music = gameObject.GetComponent<AudioSource>();
    }

    public void SetGameMode(Gamemode gm)
    {
        m_gamemode = gm;
    }
    public Gamemode GetGameMode()
    {
        return m_gamemode;
    }
}
