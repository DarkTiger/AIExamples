using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Minimax algorithm for TicTacToe, 2 players
public class TicTacToe_Minimax : MonoBehaviour
{
    public enum CellState
    {
        E = 0,
        x = 1,
        o = -1
    }

    public class StateAction
    {
        public CellState[,] state;
        public Action action;
    }

    public class Action
    {
        public int action_i = -1;
        public int action_j = -1;

        public override string ToString()
        {
            return "(" + action_i + "," + action_j + ")";
        }
    }

    public class Score
    {
        public int value;
    }

    // Game description
    static int N = 3;

    public CellState aiPlayer = CellState.x;
    public bool verbose = false;

    void Awake()
    {
        Execute();
    }

    void Execute()
    {
        // Setup the game
        CellState[,] initialState = new CellState[N, N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                initialState[i, j] = CellState.E;
            }
        }

        initialState[0, 0] = CellState.E;
        initialState[1, 0] = CellState.x;
        initialState[2, 0] = CellState.x;
        initialState[0, 1] = CellState.x;
        initialState[1, 1] = CellState.o;
        initialState[2, 1] = CellState.E;
        initialState[0, 2] = CellState.o;
        initialState[1, 2] = CellState.E;
        initialState[2, 2] = CellState.E;

        Debug.Log(PrintState(initialState));

        Action bestAction = new Action();
        StartCoroutine(GetBestAction(initialState, aiPlayer, bestAction));
    }

    IEnumerator GetBestAction(CellState[,] state, CellState aiPlayer, Action action)
    {
        Score score = new Score();
        yield return StartCoroutine(RecursiveMiniMaxing(state, aiPlayer, action, score));
        Debug.Log("YOU SHOULD PLAY: " + action.ToString());
    }

    IEnumerator RecursiveMiniMaxing(CellState[,] state, CellState player, Action action, Score score)
    {
        Debug.Log("Player " + player + " simulating state: " + PrintState(state));
        yield return null;

        // If this is a terminal state, return the value
        if (Wins(state, player))
        {
            // return a win
            score.value = 10;
            yield break;
        }
        else if (Loses(state, player))
        {
            // return a lose
            score.value = -10;
            yield break;
        }
        else if (Draws(state, player))
        {
            // return a draw
            score.value = 0;
            yield break;
        }


        // Check if mini or maxi
        bool isMaximizing = player == aiPlayer;

        if (isMaximizing)
        {
            // Maximizing
            int bestScoreValue = -100000;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    // Valid action where empty
                    if(state[i,j] == CellState.E)
                    {
                        // Test that action
                        CellState[,] nextState = (CellState[,])state.Clone();
                        nextState[i, j] = player;

                        // Get the next score
                        Score nextScore = new Score();
                        Action nextAction = new Action();
                        yield return StartCoroutine(RecursiveMiniMaxing(nextState, (CellState)((int)player * -1), nextAction, nextScore));
                        nextScore.value *= -1;

                        // Update the max
                        if (nextScore.value > bestScoreValue)
                        {
                            bestScoreValue = nextScore.value;
                            action.action_i = i;
                            action.action_j = j;
                        }
                    }
                }
            }

            Debug.Log("MAX score: " + bestScoreValue + "\nBest action: " + action);

            score.value = bestScoreValue;
        }
        else
        {
            // Minimizing
            int bestScoreValue = 100000;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    // Valid action where empty
                    if (state[i, j] == CellState.E)
                    {
                        // Test that action
                        CellState[,] nextState = (CellState[,])state.Clone();
                        nextState[i, j] = player;

                        // Get the next score
                        Score nextScore = new Score();
                        Action nextAction = new Action();
                        yield return StartCoroutine(RecursiveMiniMaxing(nextState, (CellState)((int)player * -1), nextAction, nextScore));
                        nextScore.value *= -1;

                        // Update the min
                        if (nextScore.value < bestScoreValue)
                        {
                            bestScoreValue = nextScore.value;
                            action.action_i = i;
                            action.action_j = j;
                        }
                    }
                }
            }

            Debug.Log("MIN score: " + bestScoreValue + "\nBest action: " + action);

            score.value = bestScoreValue;
        }

        Debug.Log("SCORE " + score.value + " PLAYER " + player + " ACTION " + action + " STATE " + PrintState(state));
    }


    #region Victory

    bool Wins(CellState[,] state, CellState player)
    {
        // Look for a tris
        int tot;
        int target = ((int)player) * N;

        // Cols
        for (int i = 0; i < N; i++)
        {
            tot = 0;
            for (int j = 0; j < N; j++)
            {
                if (state[i, j] == player)
                {
                    tot++;
                }
            }
            if (tot == target)
            {
                if (verbose)
                    Debug.Log(player + " wins in col " + i);
                return true;
            }
        }

        // Rows
        for (int i = 0; i < N; i++)
        {
            tot = 0;
            for (int j = 0; j < N; j++)
            {
                if (state[j, i] == player)
                {
                    tot++;
                }
            }
            if (tot == target)
            {
                if (verbose)
                    Debug.Log(player + " wins in row " + i);
                return true;
            }

        }

        // Diagonals
        tot = 0;
        for (int i = 0; i < N; i++)
            tot += (int)state[i, i];
        if (tot == target)
        {
            if (verbose)
                Debug.Log(player + " wins in diagonal 1");
            return true;
        }

        tot = 0;
        for (int i = 0; i < N; i++)
            tot += (int)state[N - 1 - i, i];
        if (tot == target)
        {
            if (verbose)
                Debug.Log(player + " wins in diagonal 2");
            return true;
        }

        return false;
    }

    bool Loses(CellState[,] state, CellState player)
    {
        CellState opponent = player == CellState.x ? CellState.o : CellState.x;

        return Wins(state, opponent);
    }
    bool Draws(CellState[,] state, CellState player)
    {
        // A draw happens only if everything is filled!
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                if (state[i, j] == CellState.E)
                    return false;

        if (verbose)
            Debug.Log(player + " DRAWS");

        // We already cheked win and loss!
        return true;
    }

    #endregion

    #region Debug

    string PrintState(CellState[,] currentState)
    {
        var s = "\n";
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                s += (currentState[j, N - 1 - i]) + " ";
            }
            s += "\n";
        }
        return s;
    }

    #endregion

}
