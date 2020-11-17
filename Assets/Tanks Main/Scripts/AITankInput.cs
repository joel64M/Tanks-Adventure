using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace NameSpaceName {

    public class AITankInput : TankInput
    {

        #region Variables
         float distbtwn;

        public Transform target;
         Vector3 min,max;
        NavMeshAgent agent;

        Vector3 destPos;

       // [Header("Adjustable AI Properties")]
         float minDisToPatrol = 5f;
         float minStoppingDist = 5f;
         float attackRadius = 20f;
         float startDecisionForNextPointTime = 0;
         float fireRate=0.2f;

        float decisionForNextPointTime ;
        float fireRateTime;
        
        STATE currentState = STATE.patrol;
      [SerializeField]  [Flags]public enum STATE { patrol, retreat, atttack ,alert }
    #endregion

    #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            if (enemyProperty)
            {
                minDisToPatrol = enemyProperty.minDisToPatrol;
                minStoppingDist = enemyProperty.minStoppingDist;
                attackRadius = enemyProperty.attackRadius;
                startDecisionForNextPointTime = enemyProperty.startDecisionForNextPointTime;
                fireRate = enemyProperty.fireRate;
                GetComponent<NavMeshAgent>().speed = enemyProperty.maxSpeed;
                GetComponent<NavMeshAgent>().acceleration = enemyProperty.maxSpeed;
                GetComponent<NavMeshAgent>().stoppingDistance = minStoppingDist;

                //double missile
                GetComponent<TankHealth>().SetTankHealth(enemyProperty.startHealth);
                GetComponent<TankMotor>().SetAiTankMotor();
                GetComponent<TankWeapon>().SetTankWeapon(enemyProperty.doubleMissile);
            }
            decisionForNextPointTime = startDecisionForNextPointTime;
            agent = GetComponent<NavMeshAgent>();
            if (GetComponent<PatrolZone>())
            {
                min.x = GetComponent<PatrolZone>().point1.x;
                min.z = GetComponent<PatrolZone>().point2.z;
                max.z = GetComponent<PatrolZone>().point1.z;
                max.x = GetComponent<PatrolZone>().point2.x;
            }

        }
        private void Start()
        {
            destPos = FindRandomPos();

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            /// Gizmos.DrawWireCube((min+max)/2,(max-min));
            Gizmos.DrawWireSphere(transform.position, attackRadius);
           // Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(destPos, 1f);
        }
   
        protected override void Update()
        {
         //   if (gm.CurrentGameState != GAMESTATE.play)
           //     return;
            if(target)
            distbtwn = Vector3.Distance(transform.position, target.position);
            if (distbtwn < attackRadius && currentState !=STATE.alert && target.gameObject.activeSelf)
            {
              currentState |= STATE.alert;
            }
            else
            {
                currentState &= ~STATE.alert;
            }
            Debug.DrawLine(transform.position, destPos, Color.red);
           
            if (currentState == STATE.patrol)
            {
                Patrol();
                IsFire = false;
            }
            else if( currentState == (STATE.patrol | STATE.alert))
            {
                Patrol();
                if (target)
                    Attack();
            }
            else
            {
                print("lol");
            }
        }

  
        #endregion

        #region Custom Methods
        void Attack()
        {
            if (target.gameObject.activeSelf)
            {
                if (fireRateTime <= 0)
                {
                    IsFire = true;
                    FirePos = target.position;
                    fireRateTime = UnityEngine.Random.Range(fireRate - 0.1f,fireRate + 0.1f);
                }
                else
                {
                    fireRateTime -= Time.deltaTime;
                    IsFire = false;
                }
            }
        }

        void Patrol()
        {
            if (Vector3.Distance(transform.position, destPos) < minStoppingDist)
            {
               // HorizontalInputValue = 0;
               // VerticalInputValue = 0;
                if (decisionForNextPointTime <= 0)
                {
                    decisionForNextPointTime = startDecisionForNextPointTime;
                    FirePos = destPos = FindRandomPos();
                }
                else
                {
                    FirePos = Vector3.zero;
                    decisionForNextPointTime -= Time.deltaTime;
                }
            }
            else
            {
                //  inputVec = destPos - transform.position;
                //  inputVec.Normalize();
                //  HorizontalInputValue = inputVec.x;
                //  VerticalInputValue = inputVec.z;
                agent.SetDestination(destPos);
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
