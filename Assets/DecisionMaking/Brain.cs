using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionMaker : MonoBehaviour
{
    public abstract void MakeDecision();
}

public class Brain : MonoBehaviour
{
    public float tickDelay = 1;

    public DecisionMaker decisionMaker;

	void Start ()
    {
        InvokeRepeating("TickBrain", tickDelay, tickDelay);
	}
	
    void TickBrain()
    {
        decisionMaker.MakeDecision();
    }
}
