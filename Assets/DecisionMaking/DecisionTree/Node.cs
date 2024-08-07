using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.DecisionTree
{
    public abstract class Node : MonoBehaviour
    {
        abstract public void MakeDecision();
    }
}
