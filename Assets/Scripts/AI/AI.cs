
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * AI can recive a difficulty as a parameter and return a random token
 * this is the computer players logic and also the hint logic.
 * 
 */
public class AI
{

    public Token DifficultySort(Difficulty Difficulty,Token[] Tokens)
    {
        switch (Difficulty)
        {
            case Difficulty.Easy:
            {
                    
                return EasyDifficultyToken(Tokens);
            }

            case Difficulty.Medium:
            {

                break;
            }

            case Difficulty.Hard:
            {
                break;
            }
            default:
                return EasyDifficultyToken(Tokens);
        }
        return null;
    }


    private Token EasyDifficultyToken(Token[] Tokens)
    {
        List<int> positions = new List<int>();

        for(int i = 0; i < Tokens.Length; i++)
        {
            if (TokenState.Empty == Tokens[i].tokenState)
                positions.Add(i);
        }
        if(positions.Count > 0)
            return Tokens[positions[Random.Range(0, positions.Count)]];
        else return null;
    }

    public Token SelectRandom(Token[] Tokens)
    {
        List<int> positions = new List<int>();

        for (int i = 0; i < Tokens.Length; i++)
        {
            if (TokenState.Empty == Tokens[i].tokenState)
                positions.Add(i);
        }
        if (positions.Count > 0)
            return Tokens[positions[Random.Range(0, positions.Count)]];
        else return null;
    }


}

