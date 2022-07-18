
using UnityEngine;
using UnityEngine.UI;

/*
 The GameFlowManager is the logic of the game and controls the 
 general flow of the game. It Creates the players and the tokens
 and puts them together to make the game using the  Setting
 as a map.
 
 The GFM starts by Creating all the Game Objects it will need in
 awake creating the tokens and placing them on the board, creating
 players(which can be both AI controlled and a player) and them randomly
 selecting who gets what token and who goes first.

from there it just swaps the player who has control and checks each turn
for a winner.

 */


public class GameFlowManager : MonoBehaviour
{
    const int TileAmount = 9;

    [SerializeField]
    [Tooltip("Prefab for Token")]
    Token m_tokenPrefab = null;

    [SerializeField]
    [Tooltip("Grid parent for tokens")]
    GridLayoutGroup m_gridLayout = null;

    [SerializeField]
    [Tooltip("Amount of time")]
    int m_time = 5;

    [SerializeField]
    [Tooltip("Game Timer")]
    Timer m_timer= null;

    [SerializeField]
    [Tooltip("Default Graphics")]
    GraphicSelector m_graphics = null;

    [SerializeField]
    [Tooltip("Has inital graphics and sets streamed graphics")]
    EndCard m_EndCard = null;


     AI m_HintAI = null;


    //player settings will have the data of what kind of game we are playing
    Player m_player1=null;
    Player m_player2=null;

    int endplayer = -1;

    Token[] m_Tokens = null;
    

    public GameState GameState { get; set; } = GameState.InProgress;

    private void Awake()
    {
   
        SetupGrid();
        SetupPlayers();
        SetupTime();
        SetupTurns();
        
    }
   
    //Setup grid makes 9 Tokens and places them on the board on a grid 3 for each grow
    public void SetupGrid()
    {
        m_HintAI = new AI();
        if(Settings.instance!=null)
        m_graphics = Settings.instance.graphicSelector;
        if (m_Tokens == null)
            m_Tokens = new Token[TileAmount];
        for(int i = 0; i < m_Tokens.Length; i++)
        {
            m_Tokens[i] = Instantiate(m_tokenPrefab);
            m_Tokens[i].transform.SetParent(m_gridLayout.transform);
            m_Tokens[i].position = i;

        }

    }
    //Creates 2 players and gives them random tokens based on the graphic
    public void SetupPlayers()
    {
        CreatePlayers();
        int rand = Random.Range(0, 2);

        if(rand == 0)
        {
            m_player1.Input.Sprite = m_graphics.GetTokenGraphic(TokenState.O);
            m_player1.Input.TokenState = TokenState.O;
            m_player2.Input.Sprite = m_graphics.GetTokenGraphic(TokenState.X);
            m_player2.Input.TokenState = TokenState.X;
        }
        else
        {
            m_player1.Input.Sprite = m_graphics.GetTokenGraphic(TokenState.X);
            m_player1.Input.TokenState = TokenState.X;
            m_player2.Input.Sprite = m_graphics.GetTokenGraphic(TokenState.O);
            m_player2.Input.TokenState = TokenState.O;
        }
        
    }
    
    public void SetupTime()
    {
        m_timer.SetTime(m_time);
        m_timer.SetTrigger(EndOfTime);
        ResetTimer();
    }

    private void ResetTimer()
    {
        m_timer.ResetTimer();
    }
    //Gives control the first turn to a random player and sets up turn swap.
    public void SetupTurns()
    {
        
        Actions.OnTurnSwap += SwapTurn;
        int rand = Random.Range(0, 2);

        if (rand == 0){
       
            m_player1.Input.EnablePlayerAction(m_Tokens);
        }
        else{
        
            m_player2.Input.EnablePlayerAction(m_Tokens);
        }

    }
    //A turn ends by Checking for a winner and resetting the clock then giving control to the other player.
    public void SwapTurn()             
    {
        CheckWinner();
        ResetTimer();
        if(GameState == GameState.InProgress) {

            if (m_player1.Input.IsTurn){
                m_player1.Input.DisablePlayerAction();
                m_player2.Input.EnablePlayerAction(m_Tokens);
                
                
                
            }
            else{     
                m_player2.Input.DisablePlayerAction();
                m_player1.Input.EnablePlayerAction(m_Tokens);
                
            }

        }
        else
        {
            m_EndCard.SetEndCard(WhoWon());
            StopGame();
        }
    }
    //The next two functions are made to check whoes turn it is and whoes turn it isnt
    public Player GetPlayerOfTurn()
    {
        if (m_player1.Input.IsTurn )
           return m_player1;
        else
           return m_player2;
    }
    public Player GetPlayerOfNextTurn()
    {
        if (!m_player1.Input.IsTurn)
            return m_player1;
        else
            return m_player2;
    }
    /*
     * CheckWinner calculates each set of 3 by summing them up
     * . If the player with X wins the tokens will 3 tokens lined up should be worth 6
     * as that is there token value, If the other player wins its worth 3. I did it this way
     * to save a alot of comparisons as math operations are faster on the compute. This should come
     * down to O(n)
     */
    public void CheckWinner()
    {
        int check;
        int player1token = (int)m_player1.Input.TokenState;
        int player2token = (int)m_player2.Input.TokenState;
        for (int i = 0; i < m_Tokens.Length; i +=3)
        {
            check = (int)m_Tokens[i].tokenState + (int)m_Tokens[i + 1].tokenState + (int)m_Tokens[i + 2].tokenState;
            if (check == 3*player1token){
               
                GameState = GameState.Player1Winner;
                return;
            }
            if (check == 3 * player2token)
            {
            
                GameState = GameState.Player2Winner;
                return;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            check = (int)m_Tokens[i].tokenState + (int)m_Tokens[i + 3].tokenState + (int)m_Tokens[i + 6].tokenState;
            if (check == 3 * player1token)
            {
                GameState = GameState.Player1Winner;
                return;
            }
            if (check == 3 * player2token)
            {
                GameState = GameState.Player2Winner;
                return;
            }
        }
        
        if((int)m_Tokens[0].tokenState + (int)m_Tokens[4].tokenState + (int)m_Tokens[8].tokenState == 3 * player1token ||
            (int)m_Tokens[2].tokenState + (int)m_Tokens[4].tokenState + (int)m_Tokens[6].tokenState == 3 * player1token)
        {
            GameState = GameState.Player1Winner;
            return;
        }

        if ((int)m_Tokens[0].tokenState + (int)m_Tokens[4].tokenState + (int)m_Tokens[8].tokenState == 3 * player2token ||
            (int)m_Tokens[2].tokenState + (int)m_Tokens[4].tokenState + (int)m_Tokens[6].tokenState == 3 * player2token)
        {
            GameState = GameState.Player2Winner;
            return;
        }
        check = 0;
        for(int i = 0; i < m_Tokens.Length; i++)
        {
            check += (int)m_Tokens[i].tokenState;
        }
        if (check == 13 || check == 14)
            GameState = GameState.Draw;

    }
    //This function just reloads or clears the data of most of the pieces.
    public void ResetGame()
    {
        
        ResetActions();
        ResetTokens();
        ResetPlayers();
        SetupPlayers();
        ResetTimer();
        GameState = GameState.InProgress;
        m_EndCard.ResetEndCard();
        SetupTurns();

    }
    // The next functions are all reset functions that reset values or recreates objects them.
    private void ResetTokens()
    {
        for (int i = 0; i < m_Tokens.Length; i++)
            m_Tokens[i].Clear();
            
    }
    private void ResetPlayers()
    {
        m_player1.Input.IsTurn = false;
        m_player2.Input.IsTurn = false;

    }
    private void ResetActions()
    {
        Actions.OnTokenPress = null;
        Actions.OnTurnSwap = null;
    }
    //selects a winner if the game time ends
    private void EndOfTime()
    {
        if (m_player1.Input.IsTurn)
        {
            GameState = GameState.Player2Winner;
        }
        else
        {
            GameState = GameState.Player1Winner;
        }
        StopGame();
    }
    //ends the game after a winner is declared
    public void StopGame()
    {
        
        m_EndCard.SetEndCard(WhoWon());
        m_timer.Pause();
        if (m_player1.Input.IsTurn)
        {
            m_player1.Input.DisablePlayerAction();
            endplayer = 1;
        }
        else
        {
            m_player2.Input.DisablePlayerAction();
            endplayer = 2;
        }

    }
    //checks who won for if that is needed.
    public string WhoWon()
    {

        switch (GameState)
        {
            case GameState.Player1Winner:
                return "Player 1 Wins";
            case GameState.Player2Winner:
                return "Player 2 Wins";
            case GameState.Draw:
                return "Draw";
            case GameState.InProgress:
                return "In Progress";
            default:
                break;
        }
        return "";
    }
    //The Undo function has different functionality depending on gamemode
    //if in player vs player the undo will only reset one turn back from the player that clicked it.
    //if in player vs computer it will reset two turns back.
    //I checks the difference based on if the player is a computer or not so if futher player vs player functions 
    // are added it wont affect this undo.
    public void Undo()
    {
        ResetTimer();
        m_EndCard.ResetEndCard();
        if (GameState.InProgress != GameState) {
            
            GameState = GameState.InProgress;
            if(endplayer == 2) { 
                m_player1.Input.EnablePlayerAction();
            }
            if(endplayer == 1){
                m_player2.Input.IsTurn =true;
            }
        }
      
        Player player1 = GetPlayerOfTurn();
        Player player2 = GetPlayerOfNextTurn();
        if (!(player1.IsComputer)) { 
            if (!(player2.IsComputer) && !player2.PlaysIsEmpty() )
            {
                
                m_Tokens[player2.PopLastPosition()].Clear();
                SwapTurn();
            }
            else
            {
                if (!player1.PlaysIsEmpty() && !player2.PlaysIsEmpty())
                {
                    m_Tokens[player2.PopLastPosition()].Clear();
                    m_Tokens[player1.PopLastPosition()].Clear();
                }

            }
        }
        else { 
            if(endplayer != -1)
            {
               
                m_Tokens[player2.PopLastPosition()].Clear();
                m_player2.Input.DisablePlayerAction();
                SwapTurn();
                endplayer = -1;
            }
        }

    }
    //uses the AI to select a token to trigger the hint on.
    // the AI can be expended to select more cleverly.
    public void Hint()
    {
      
        if (GameState.InProgress == GameState)
        {
            m_HintAI.SelectRandom(m_Tokens).TriggerHint();
        }
    }
    //creates players based on play mode
    private void CreatePlayers()
    {
        Gamemode mode = Gamemode.PvP;
        if (Settings.instance != null)
            mode = Settings.instance.GetGameMode();
        switch (mode) {
            
            case Gamemode.PvP:
              {
                    m_player1 = new Player();
                    m_player2 = new Player();
                    break;
              }
            case Gamemode.PvC:
                {
                    m_player1 = new Player();
                    m_player2 = new Player(true);
                    break;
                }
            case Gamemode.CvC:
                {
                    m_player1 = new Player(true);
                    m_player2 = new Player(true);
                    break;
                }
            default:
                break;

        }

    }

    
}
