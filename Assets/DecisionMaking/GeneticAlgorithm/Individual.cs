using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI.Genetics
{
    public class Genotype
    {
        public int bodySizeX = 1;
        public int bodySizeY = 1;
        public int finSizeX = 1;
        public int finSizeY = 1;
        public int motorForce = 10;

        // Fitness
        public float fitness = -1000;
    }

    public class Individual : MonoBehaviour
    {
        public HingeJoint2D joint;
        public Rigidbody2D body;
        public Rigidbody2D fin;
        public Genotype genotype;

        void Awake()
        {
            SetGenotype(new Genotype());
        }
        
        public void SetGenotype(Genotype g)
        {
            genotype = g;

            // ... use the genotype to set parameters
            body.transform.localScale = new Vector3(g.bodySizeX, g.bodySizeY, 1);
            fin.transform.localScale = new Vector3(g.finSizeX, g.finSizeY, 1);
            var motor = joint.motor;
            motor.motorSpeed = g.motorForce;
            joint.motor = motor;
        }
    }
}
