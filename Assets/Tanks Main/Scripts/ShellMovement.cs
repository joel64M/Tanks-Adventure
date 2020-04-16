using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    public class ShellMovement : MonoBehaviour
    {

        #region Variables
        public float speed;
        public float range;

        bool moveShell;
        Rigidbody rb;
       public float distCovered;
        Vector3 initialPos;
    #endregion

    #region Builtin Methods

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void OnEnable()
        {
            initialPos = transform.position;
            moveShell = true;
        }

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        void FixedUpdate()
        {
            if (moveShell)
            {
                MoveShell();
            }
        }

        /*
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
        private void StopShell()
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        void MoveShell()
        {
            distCovered = Vector3.Distance(initialPos, transform.position);
            rb.MovePosition(rb.position + transform.forward * Time.deltaTime * speed);
            if (distCovered > range)
            {
                StopShell();
            }
        }
        #endregion

    }
}
