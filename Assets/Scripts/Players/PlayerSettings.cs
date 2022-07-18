using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings 
{
    readonly bool IsComputer = false;
    public TokenState tokenState { get; private set; } = TokenState.Empty;

    readonly Difficulty difficulty = Difficulty.Easy;

    PlayerSettings(bool isComputer, TokenState tokenState, Difficulty difficulty)
    {
        IsComputer = isComputer;
        this.tokenState = tokenState;
        this.difficulty = difficulty;
    }

    public void SetToken(TokenState State)
    {
        tokenState = State;
    }


}
