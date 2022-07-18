using UnityEngine;
using UnityEngine.UI;

/*****************************
 Here in Token we have triggers that cause
the animations created with the animator. Most of token's
functionality is just triggering animation events and changing token
state so the rest of the system knows if it was "pressed" or not.

I tried to keep Token Open for extension and closed for modification as possible
as is one SOLID principles. Along with trying to keep most of its functions single 
responsibilty. Ultimatly a token comes in a set of 9 and can triggers animations to fit
its state.

It action will be triggered when the token is pressed (OnButtonClick) influencing whatever was assigned to it.
 ******************************/
public class Token : MonoBehaviour
{

    readonly int m_animatorResetTrigger = Animator.StringToHash("Reset");
    readonly int m_animatorHintTrigger = Animator.StringToHash("Hint");
    readonly int m_animatorPlacedInt = Animator.StringToHash("Placed");

    Animator m_animator = null;
    Image m_image = null;
    public int position = -1;
    
    
    public TokenState tokenState { get; private set; } = TokenState.Empty;
    
    

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
        m_animator = GetComponent<Animator>();
        m_image = GetComponent<Image>();
        
        
    }

   
    private void OnButtonClick()
    {
        //can only be triggered if the token is empty
        if (tokenState == TokenState.Empty)
        {   //will be assigned to a player pressed, I did this so that in
            //case for multiplayer with multiple controllers the press would only be 
            //assigned to the correct player without constant additions of checks.
            Actions.OnTokenPress?.Invoke(this);

        }
    }
    
    
    public void SetImage(Sprite sprite)
    {
        
        m_image.sprite = sprite;
        m_animator.SetInteger(m_animatorPlacedInt, 1);
    }
    public void SetTokenState(TokenState state)
    {
        tokenState = state;
    }

    public void Clear()
    {   
        tokenState = TokenState.Empty;
        m_animator.SetInteger(m_animatorPlacedInt, -1);
        m_animator.SetTrigger(m_animatorResetTrigger);

    }
    //will trigger a aniamation that causes the token to glow nicely.
    public void TriggerHint()
    {
        m_animator.SetTrigger(m_animatorHintTrigger);

    }
}
