using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LoseTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void LoseTestPasses()
    {

        var gameobject = new GameObject();
        GameFlowManager gameflowManager = gameobject.AddComponent<GameFlowManager>();

        gameflowManager.GameState = GameState.Player2Winner;


        Assert.AreEqual("Player 2 Wins", gameflowManager.WhoWon());
    }

}
