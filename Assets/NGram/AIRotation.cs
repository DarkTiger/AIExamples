using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRotation : AIPlayer
{
    public Action rotatingAction;

    public override Action GetAction()
    {
        rotatingAction += 1;
        if (rotatingAction == (Action)3) rotatingAction = (Action)1;
        return rotatingAction;
    }
}
