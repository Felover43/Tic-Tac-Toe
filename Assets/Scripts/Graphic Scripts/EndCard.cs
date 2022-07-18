
using UnityEngine;
using UnityEngine.UI;


//animation functionality for the ending screen, if the player wins it will trigger the animation for victory
//and if a reset it is called it will trigger the reset for the animation taking it to idle.
public class EndCard : MonoBehaviour
{
    readonly int m_animatorResetTrigger = Animator.StringToHash("Reset");
    readonly int m_animatorPlacedBool = Animator.StringToHash("Ended");

    Animator m_animator = null;
    Text m_text = null;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_text = GetComponent<Text>();
    }

    public void ResetEndCard()
    {
        m_animator.SetBool(m_animatorPlacedBool, false);
        m_animator.SetTrigger(m_animatorResetTrigger);
        m_text.text = "";

    }
    public void SetEndCard(string String)
    {
        m_text.text = String;
        m_animator.SetBool(m_animatorPlacedBool, true);
    }
}
