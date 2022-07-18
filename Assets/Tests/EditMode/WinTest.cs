using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WinTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void WinTestPass()
    {
        var gameobject = new GameObject();
        GameFlowManager gameflowManager = gameobject.AddComponent<GameFlowManager>();

        gameflowManager.GameState = GameState.Player1Winner;


        Assert.AreEqual("Player 1 Wins", gameflowManager.WhoWon());
    }

    
}
