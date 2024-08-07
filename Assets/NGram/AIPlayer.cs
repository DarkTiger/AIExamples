using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPlayer : MonoBehaviour
{
    public abstract Action GetAction();

    public virtual void ReceiveOpponentAction(Action a){}
}
