using UnityEngine;



//more functionality for the game
public class Options : MonoBehaviour
{



    public void SetVolume(float volume)
    {

        Settings.instance.music.volume = volume;
    }

    public void SetFullScreen(bool isFull)
    {
        if (Screen.fullScreen)
        {
            Screen.fullScreen = isFull;
        }
        else
        {
            Screen.fullScreen = false;
        }
         
    }
}
