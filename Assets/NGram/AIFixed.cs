using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFixed : AIPlayer
{
    public Action fixedAction;

    public override Action GetAction()
    {
        return fixedAction;
    }
}
