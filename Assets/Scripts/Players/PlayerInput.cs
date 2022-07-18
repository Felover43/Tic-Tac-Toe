
using System.Collections.Generic;
using UnityEngine;


/*
 * Inheirts from an interface as to ensure that all players have the same structure
 * but use ReadInput differently.PlayerInput must be enabled to act. 
 * Once a button is selected and player input is enabled readinput influences the tokens,
 * and stores the position if undo is needed.
 * 
 */
public class PlayerInput : IPlayInput
{
   
    public void ReadInput(Token Token)
    {
        
        if (!IsTurn)        
            return;  

        Token.SetImage(Sprite);
        Token.SetTokenState(TokenState);
        plays.Add(Token.position);
        Actions.OnTurnSwap();

    }

    public void EnablePlayerAction(Token[] tokens = null)
    {
        
        IsTurn = true;
        Actions.OnTokenPress += ReadInput;

    }
    public void DisablePlayerAction(Token[] tokens = null)
    {
        IsTurn = false;
        Actions.OnTokenPress -= null;
    }
    public List<int> plays { get; set; }
    public TokenState TokenState { get; set; }
    public Sprite Sprite { get; set; }

    public bool IsTurn { get; set; }


}
