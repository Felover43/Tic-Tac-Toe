using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * A nice way to access key features of the game.
 * 
 */
public static class Actions
{
    public static Action<Token> OnTokenPress;
    public static Action OnTurnSwap;


    public static void ResetActions()
    {
        OnTokenPress= null;
        OnTurnSwap = null;
    }
}
