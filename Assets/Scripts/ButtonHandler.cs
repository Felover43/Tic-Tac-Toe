
using UnityEngine;
using UnityEngine.SceneManagement;
//Used to give different functionality to some of the buttons.
public class ButtonHandler : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The Canvas/Gameflow manager the button will be acting on")]
    GameFlowManager m_gameFlowManager =null;

    public void ResetGame()
    {
        m_gameFlowManager.ResetGame();
    
    }
    public void Undo()
    {
        m_gameFlowManager.Undo();
    }

    public void Hint()
    {
        
        m_gameFlowManager.Hint();
    }
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    
}
