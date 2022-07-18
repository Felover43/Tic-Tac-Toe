
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;


//adds more functionality to the main menu, the only interesting
//function is LoadStreamFileNames, it creates buttons from the names of assetbundles
//and adds them to the reskin layout.
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab for Button")]
    ReskinButtons m_ButtonPrefab = null;

    [SerializeField]
    [Tooltip("Grid parent for reskins")]
    LayoutGroup m_Layout = null;


    List<ReskinButtons> m_Buttons = null;

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadStreamFileNames()
    {
        //resets it if it isnt reset
        if(m_Buttons != null)
        {
            m_Buttons.Clear();
            
        }
        else
        {
            m_Buttons = new List<ReskinButtons>();
        }

        
        DirectoryInfo directory = new DirectoryInfo("Assets/StreamingAssets");
        FileInfo[] files = directory.GetFiles("*.*");
        int i = 0;
        foreach (FileInfo f in files)
        {
            //making sure only the correct and wanted assets appear on the button layout
            if (!f.ToString().Contains(".") && !Path.GetFileNameWithoutExtension(f.ToString()).Contains("StreamingAssets"))
            {
                
                m_Buttons.Add(Instantiate(m_ButtonPrefab));
                m_Buttons[i].SetFile(Path.GetFileNameWithoutExtension(f.ToString()), f.ToString());
                m_Buttons[i].transform.SetParent(m_Layout.transform);
                i++;
            }
            
            
        }
    }
    //clears the reskin layout on leaving the menu.
    public void ClearLayout()
    {
        for(int i = 1; i < m_Layout.transform.childCount; i++)
        {
            Destroy(m_Layout.transform.GetChild(i).gameObject);
        }
    }

    public void ReloadBG()
    {
        Settings.instance.graphicSelector.UpdateBackground();
    }
    public void SetPlayMode(int playmode)
    {
        switch (playmode)
        {
            case 0:
                Settings.instance.SetGameMode(Gamemode.PvP);
                break;
            case 1:
                Settings.instance.SetGameMode(Gamemode.PvC);
                break;
            case 2:
                Settings.instance.SetGameMode(Gamemode.CvC);
                break;
            default:
                break;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
