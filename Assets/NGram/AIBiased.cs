using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBiased : AIPlayer
{
    public Action biasedAction;
    [Range(0,1)]
    public float biasProbability = 0.1f;

    public override Action GetAction()
    {
        if (Random.value <= biasProbability)
        {
            return biasedAction;
        }
        return (Action)UnityEngine.Random.Range(1, 4);
    }
}
