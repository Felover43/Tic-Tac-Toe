using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DrawTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void DrawTestSimplePasses()
    {

        var gameobject = new GameObject();
        GameFlowManager gameflowManager = gameobject.AddComponent<GameFlowManager>();

        gameflowManager.GameState = GameState.Draw;


        Assert.AreEqual("Draw", gameflowManager.WhoWon());
    }


}
