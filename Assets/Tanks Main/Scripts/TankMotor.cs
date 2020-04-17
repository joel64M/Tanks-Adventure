using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    [RequireComponent(typeof(TankInput))]
    public class TankMotor : MonoBehaviour
    {

        #region Variables
        [Header("Motor Properties")]
        public float moveSpeed = 5f;
        public float forceMultiplier = 50f;
        public float maxSpeed = 10f;

        //components
        TankInput tankInputScript;
        Rigidbody rb;
        #endregion

        #region Builtin Methods

        void Awake()
        {
            tankInputScript = GetComponent<TankInput>();
            rb = GetComponent<Rigidbody>();
        }

        void OnEnable()
        {

        }

        void Start()
        {
            
        }

        void Update()
        {
            if (rb)
            {
                MoveByForce();
            }
        }
        /*
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
        */
        #endregion

        #region Custom Methods

        void Move()
        {
            Vector3 movement = transform.forward * tankInputScript.VerticalInputValue * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + movement);
        }

        void MoveByForce()
        {
            // Vector2 ip = new Vector2(Joystick)
            Vector3 input = new Vector3(tankInputScript.HorizontalInputValue, 0, tankInputScript.VerticalInputValue);
            transform.LookAt(transform.position + input);

            rb.AddForce(input * forceMultiplier);
          
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        #endregion

    }
}
