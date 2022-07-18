using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UndoTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void UndoTestPasses()
    {
        var gameobject = new GameObject();

        Token token = gameobject.AddComponent<Token>();
        Player player = new Player();
        token.position = 1; 
        player.Input.plays.Add(token.position);
        Assert.AreEqual(1, player.PopLastPosition());


    }

   
}
