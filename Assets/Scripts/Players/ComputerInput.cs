using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Works similarly to playerinput but ReadInput use an Item selected by the AI class
 * instead of waits for a button press. It waits a delay as to seem more natural. 
 */
public class ComputerInput : IPlayInput
{

    //maybe add a init function to both with some kind of player settings that also has difficulty
    public void ReadInput(Token Token = null)
    {

        if (!IsTurn)
            return;
        if (Token == null)
           Actions.OnTurnSwap();
        Token.StartCoroutine(DelayActions(1, Token));

        
       
    }
    private IEnumerator DelayActions(float Delay, Token Token)
    {
        
        yield return new WaitForSeconds(Delay);
        if (!IsTurn)
            yield break;
        Token.SetImage(Sprite);
        Token.SetTokenState(TokenState);
        plays.Add(Token.position);
        Actions.OnTurnSwap();
    }
    private void AIControl(Token[] Tokens)
    {
       
        ReadInput(AI.DifficultySort(Difficulty, Tokens));
    }
   
    public void EnablePlayerAction(Token[] Tokens =null)
    {
        IsTurn = true;
        AIControl(Tokens);

    }
    public void DisablePlayerAction(Token[] Tokens = null)
    {
        IsTurn = false;
       
       
    }
    public List<int> plays { get; set; }
    public TokenState TokenState { get; set; }
    public Sprite Sprite { get; set; }

    Difficulty Difficulty { get; set; } = Difficulty.Easy;

    readonly AI AI = new AI();
    public bool IsTurn { get; set; }



}