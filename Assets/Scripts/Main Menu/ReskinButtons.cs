using UnityEngine;
using System.IO;
using UnityEngine.UI;
/*
 * ReskinButtons are how I implemented part 3 and 4
 * rather then a textbox I decided to add buttons that just put 
 * all the assets on at runtime.
 * The name of the reset button is taken from the name of the asset bundle so its easy to change
 * or create.
 * and once pressed it will move all the assets to Settings for the game.
 */
public class ReskinButtons : MonoBehaviour
{

    Text m_Text = null;


    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
        m_Text = GetComponentInChildren<Text>();
    }


    public void  SetFile(string text, string file)
    {
        m_Text.text = text;

    }
    public void OnButtonClick()
    {
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, m_Text.text));
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }

        Object[] assets = myLoadedAssetBundle.LoadAllAssets();
         
        Settings.instance.graphicSelector.SetGraphics(  (Sprite)assets[5],(Sprite)assets[1],(Sprite)assets[3]);
        Settings.instance.graphicSelector.UpdateBackground();

        myLoadedAssetBundle.Unload(false);
    }


}