
using System.Collections.Generic;
using UnityEngine;

/*
 * An interface to base the players on, this way no matter what type of player
 * is extended to this game it will always have the same base needed to influence the game.
 * This allows player to be created for all game modes and its functionality will change but its
 * name wont.
 */
public interface IPlayInput 
{
    void ReadInput(Token Token);
    TokenState TokenState { get; set; }
    Sprite Sprite { get; set; }
    List<int> plays { get; set; }
    bool IsTurn { get; set; }

    void EnablePlayerAction(Token[] tokens = null);
    void DisablePlayerAction(Token[] tokens = null);

}
