using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HintTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void Hint()
    {
        var gameObject = new GameObject();
        Token[] token = new Token[9];
        for(int i = 0; i < 9; i++)
        {
            token[i] = gameObject.AddComponent<Token>();
        }
        AI aI = new AI();
        Assert.AreEqual(typeof(Token), aI.SelectRandom(token).GetType());
    }


}
