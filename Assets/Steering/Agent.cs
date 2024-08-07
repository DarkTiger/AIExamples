using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Movement
{

    public class Agent : MonoBehaviour
    {
        // Parameters
        public float maximumLinearVelocity = 1;
        public float maximumAngularVelocity = 90;

        public float maximumLinearAcceleration = 1;
        public float maximumLinearDeceleration = 1;
        public float maximumAngularAcceleration = 90;

        // State
        private Vector2 linearVelocity;
        private float angularVelocity;

        private float currentLinearVelocity;

        private float linearAcceleration;
        private float angularAcceleration;


        private SteeringBehaviour[] steeringBehaviours;

	    void Start ()
        {
            steeringBehaviours = gameObject.GetComponents<SteeringBehaviour>();
        }

        void Update ()
        {
            // Get the steering output
            Vector2 totalSteering = Vector2.zero;
            float totalWeight = 0;
            foreach(SteeringBehaviour steeringBehaviour in steeringBehaviours)
            {
                totalSteering += steeringBehaviour.GetSteering().targetLinearVelocityPercent * steeringBehaviour.weight;
                totalWeight += steeringBehaviour.weight;
            }
            totalSteering = totalSteering / totalWeight;

            // Set accelerations based on steering target
            var targetVelocityPercent = totalSteering; // [0,1]
            var targetVelocity = targetVelocityPercent * maximumLinearVelocity;

            if (targetVelocity.sqrMagnitude > currentLinearVelocity * currentLinearVelocity)
            {
                linearAcceleration = maximumLinearAcceleration;
            }
            else if (targetVelocity.sqrMagnitude < currentLinearVelocity * currentLinearVelocity)
            {
                linearAcceleration = -maximumLinearDeceleration;
            }
            else
            {
                linearAcceleration = 0;
            }

            Vector3 crossVector = Vector3.Cross(targetVelocityPercent, transform.right);
            float angle = Mathf.Abs(Vector3.Angle(targetVelocityPercent, transform.right));
            int rotationDirection = -(int)Mathf.Sign(crossVector.z);

            float accelerationRatio = Mathf.Clamp(angle, 0, 5) / 5.0f;
            angularAcceleration = rotationDirection * maximumAngularAcceleration * accelerationRatio;

            // Velocity update
            currentLinearVelocity  += linearAcceleration * Time.deltaTime;
            currentLinearVelocity = Mathf.Clamp(currentLinearVelocity, 0, maximumLinearVelocity);
            linearVelocity = (Vector2)transform.right * currentLinearVelocity;

            angularVelocity += angularAcceleration * Time.deltaTime;
            angularVelocity = Mathf.Clamp(angularVelocity, -maximumAngularVelocity, maximumAngularVelocity);
            angularVelocity = angularVelocity * accelerationRatio;


            // Position / rotation update
            transform.position += (Vector3)(linearVelocity * Time.deltaTime);
            transform.localEulerAngles += Vector3.forward * angularVelocity * Time.deltaTime;

            Debug.DrawLine(transform.position, transform.position + (Vector3)linearVelocity, Color.red);
            Debug.DrawLine(transform.position, transform.position + (Vector3)totalSteering, Color.green);
        }


    }

}