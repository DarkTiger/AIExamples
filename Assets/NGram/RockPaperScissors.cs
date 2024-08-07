using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    NONE = 0,
    ROCK = 1,
    PAPER = 2,
    SCISSORS = 3
}

/// <summary>
/// RPS gameplay
/// - input R P or S to play
/// - the AI will select the action
/// - we log the result
/// </summary>
public class RockPaperScissors : MonoBehaviour {

    #region Statistics

    int nGames = 0;
    int nWon = 0;
    int nLost = 0;
    int nDraw = 0;

    #endregion

    public int maxGames = 100;

    public AIPlayer player1;
    public AIPlayer player2;

    void Start()
    {
        //player1 = gameObject.AddComponent<AIRandom>();
        //player2 = gameObject.AddComponent<AIRandom>();
    }

    void Update () {

        Action p1Action = Action.NONE;
        //if (GetInput(out chosenAction))
        if (nGames < maxGames)
        {
            // You choose
            p1Action = player1.GetAction();
            Debug.Log("You choose: " + p1Action);

            // AI chooses
            Action p2Action = player2.GetAction();
            Debug.Log("AI chooses: " + p2Action);

            // Inform the AIs of the choices
            player1.ReceiveOpponentAction(p2Action);
            player2.ReceiveOpponentAction(p1Action);

            // Check who wins
            int diff = ((int)p1Action - (int)p2Action);
            if (diff == 1 || diff == -2)
            {
                Debug.Log("YOU WIN!");
                nWon++;
            }
            else if (diff == 2 || diff == -1)
            {
                Debug.Log("YOU LOSE");
                nLost++;
            }
            else
            {
                Debug.Log("DRAW");
                nDraw++;
            }
            nGames++;

            float myWinRate = nWon * 100 / (float)nGames;
            float aiWinRate = nLost * 100 / (float)nGames;
            Debug.Log("My winrate: " + myWinRate.ToString("0.00") + "%"
                +  "    AI winrate: " + aiWinRate.ToString("0.00") + "%" 
                + "\n nGames: " + nGames + " nWon: " + nWon + " nLost: " + nLost + " nDraw: " + nDraw);
        }
    }

    bool GetInput(out Action chosenAction)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            chosenAction = Action.ROCK;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            chosenAction = Action.PAPER;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            chosenAction = Action.SCISSORS;
            return true;
        }
        chosenAction = Action.NONE;
        return false;
    }

}
