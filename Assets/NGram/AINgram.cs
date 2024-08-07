using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AINgram : AIPlayer
{
    public int nGamesBeforePrediction = 20;

    Dictionary<string, int[]> ngramTable;
    List<Action> actionsWindow;

    public void Awake()
    {
        ngramTable = new Dictionary<string, int[]>();

        // Create the pairs
        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                var key = i + "" + j;
                var value = new int[3];
                ngramTable.Add(key,value);
            }
        }

        // Initialise the queue
        actionsWindow = new List<Action>();
    }



    public override void ReceiveOpponentAction(Action a)
    {
        // Update the table 
        if (actionsWindow.Count >= 2)
        {
            Action a0 = actionsWindow[0];
            Action a1 = actionsWindow[1];
            var key = (int)a0 + "" + (int)a1;
            ngramTable[key][(int)a - 1] += 1;
        }

        // Add to the history
        actionsWindow.Add(a);
        if (actionsWindow.Count == 3)
        {
            actionsWindow.RemoveAt(0);
        }
    }

    private int nGames = 0;
    public override Action GetAction()
    {
        nGames++;
        if (nGames < nGamesBeforePrediction)
        {
            return (Action)UnityEngine.Random.Range(1, 4);
        }
        else
        {
            // Use the table to predict the next action!
            Action a0 = actionsWindow[0];
            Action a1 = actionsWindow[1];
            var key = (int)a0 + "" + (int)a1;
            int[] opponentActionCounts = ngramTable[key];
            int totalOpponentActions = opponentActionCounts.Sum();

            float[] ourActionProbabilities = new float[3];
            ourActionProbabilities[(int)(Action.ROCK)-1] = opponentActionCounts[(int)(Action.SCISSORS) - 1] / (float)totalOpponentActions;
            ourActionProbabilities[(int)(Action.PAPER) - 1] = opponentActionCounts[(int)(Action.ROCK) - 1] / (float)totalOpponentActions;
            ourActionProbabilities[(int)(Action.SCISSORS) - 1] = opponentActionCounts[(int)(Action.PAPER) - 1] / (float)totalOpponentActions;

            // Randomly choose based on these probabilities
            float rndChoice = UnityEngine.Random.value;
            if (rndChoice <= ourActionProbabilities[(int)(Action.ROCK) - 1])
            {
                return Action.ROCK;
            }
            else if (rndChoice <= ourActionProbabilities[(int)(Action.ROCK) - 1] + ourActionProbabilities[(int)(Action.PAPER) - 1])
            {
                return Action.PAPER;
            }
            else
            {
                return Action.SCISSORS;
            }
        }
    }
}
