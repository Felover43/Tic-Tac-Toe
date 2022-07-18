
using System.Collections.Generic;
/*
 * Player is the controller, I wanted to use dependency inversion
 * for the player as it made sense. So player uses input from Iplayer/Icomputer
 * which both inherit from an interface. This allows for the ability to change players functionality
 * intierly based on the type of player, so for instance the computer will use AI and player will use button
 * presses to select a token.
 * 
 * This functionallity also opens the door for more player types like online multiplayer implementations by just
 * creating a different Ionlineplayer or so without needing to ouch Iplayer. Keeping it open for extenion but closed
 * for modification.
 * 
 */
public class Player {

    public IPlayInput Input;
    public bool IsComputer = false;
    


    public Player()
    {
        
        Input = IsComputer ?
            new ComputerInput() as IPlayInput :
            new PlayerInput();

        Input.IsTurn = false;
        Input.plays = new List<int>();

      
    }
    public Player(bool AI)
    {
       
        IsComputer = AI;
       
        Input = IsComputer ?
            new ComputerInput() as IPlayInput :
            new PlayerInput();

        Input.IsTurn = false;
        Input.plays = new List<int>();

    }

    public int PopLastPosition()
    {
        if (Input.plays.Count == 0)
            return -1;
        int value = Input.plays[Input.plays.Count - 1];
        Input.plays.RemoveAt(Input.plays.Count - 1);
        return value;
    }

    public bool PlaysIsEmpty()
    {
        if (Input.plays.Count == 0)
            return true;
        return false;
    }

   
   

}
