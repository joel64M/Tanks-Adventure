using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    public class ShellMovement : MonoBehaviour
    {
        //public static bool isBook;

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
         //   isBook = true;
        }

        void OnEnable()
        {
            initialPos = transform.position;
            moveShell = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }

        void FixedUpdate()
        {
            if (moveShell)
            {
                MoveShell();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            StopShellMovement();
        }
        #endregion

        #region Custom Methods
        void StopShellMovement()
        {
            moveShell = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void ShellDropDown()
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        void MoveShell()
        {
            distCovered = Vector3.Distance(initialPos, transform.position);
            rb.MovePosition(rb.position + transform.forward * Time.deltaTime * speed);
            if (distCovered > range)
            {
                ShellDropDown();
            }
        }
        #endregion

    }
}
