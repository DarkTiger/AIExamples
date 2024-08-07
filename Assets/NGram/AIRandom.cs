using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRandom : AIPlayer
{
    public override Action GetAction()
    {
        return (Action)UnityEngine.Random.Range(1, 4);
    }
}
