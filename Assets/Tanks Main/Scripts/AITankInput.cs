using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    public class AITankInput : TankInput
    {

        #region Variables
        public float distbtwn;

        public Transform target;
        public Vector3 inputVec;
        public Vector3 min,max;

        Vector3 destPos;
        [Header("Adjustable AI Properties")]
        public float minDisToPatrol = 5f;
        public float minStoppingDist = 5f;
        public float minDistToTriggerAttack = 20f;

        //patrol
        float waitTime ;
        public float startWaitTime = 2f;

        //attack
        float attackAfterTime;
        STATE currentState = STATE.patrol;
        [Flags]public enum STATE { patrol, retreat, atttack ,alert }
    #endregion

    #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            destPos = FindRandomPos();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            waitTime = startWaitTime;

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((min+max)/2,(max-min));
        }
   
        protected override void Update()
        {

            distbtwn = Vector3.Distance(transform.position, target.position);
            if (distbtwn < minDistToTriggerAttack && currentState !=STATE.alert)
            {
              currentState |= STATE.alert;
                print("atadg");

            }
            Debug.DrawLine(transform.position, destPos, Color.blue);
            if (currentState == STATE.patrol)
            {
                Patrol();
            }
            else if( currentState == (STATE.patrol | STATE.alert))
            {
                Patrol();
                Attack();
            }
            else
            {
                print("lol");
            }
        }

        void FixedUpdate()
        {
            
        }

        void LateUpdate()
        {

        }

        void OnDisable()
        {

        }

        void Destroy()
        {

        }

        #endregion

        #region Custom Methods
        void Attack()
        {
            if (attackAfterTime <= 0)
            {
                IsFire = true;
                FirePos = target.position;
                attackAfterTime = UnityEngine.Random.Range(0.5f, 2f);
            }
            else
            {
                attackAfterTime -= Time.deltaTime;
                IsFire = false;
            }
        }

        void Patrol()
        {
            if (Vector3.Distance(transform.position, destPos) < minStoppingDist)
            {
                HorizontalInputValue = 0;
                VerticalInputValue = 0;
                if (waitTime <= 0)
                {
                    waitTime = startWaitTime;
                    FirePos = destPos = FindRandomPos();
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else
            {
                inputVec = destPos - transform.position;
                inputVec.Normalize();
                HorizontalInputValue = inputVec.x;
                VerticalInputValue = inputVec.z;
            }
        }
      
        Vector3 FindRandomPos()
        {
            Vector3 temp = new Vector3(UnityEngine.Random.Range(min.x, max.x), 0, UnityEngine.Random.Range(min.z, max.z));
            if (Vector3.Distance(transform.position, temp) < minDisToPatrol)
            {
               return FindRandomPos();
            }
            else
            {
                return temp;
            }
        }
    #endregion

    }
}
