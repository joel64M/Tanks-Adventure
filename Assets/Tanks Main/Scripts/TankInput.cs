using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace NameSpaceName {

    public class TankInput : MonoBehaviour
    {

        #region Variables
        //public exposed
        ///public LayerMask ignoreLayer;
         //read only inputs

        public float HorizontalInputValue
        {
            get;
            protected set;
        }
        public float VerticalInputValue
        {
            get;
            protected set;
        }
        public bool IsFire
        {
            protected set;
            get;
        }
        public Vector3 FirePos
        {
            protected set;
            get;
        }

        //comopnents
        protected Camera cam;
        Vector3 hitPos;
        #endregion

        #region Builtin Methods

        protected virtual void  Awake()
        {
            cam = Camera.main;
        }

        protected virtual  void Update()
        {
            if(cam)
            HandleFireInputs();
            HandleMovementInputs();
        }

        #endregion

        #region Custom Methods

        protected virtual void HandleMovementInputs()
        {
            VerticalInputValue = Input.GetAxis("Vertical");
            HorizontalInputValue = Input.GetAxis("Horizontal");
        }

        protected virtual void HandleFireInputs()
        {
            if (Input.GetMouseButton(0))
            {
                IsFire = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                IsFire = false;
            }
            FindFirePos(Input.mousePosition);
        }

        protected virtual void FindFirePos(Vector2 mousePos)
        {
            Ray ray = cam.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                //if ((int)Mathf.Pow(2, hit.transform.gameObject.layer) == (int)layermask)
                {
                    hitPos = hit.point;
                    hitPos.y = 0f;
                    FirePos = hitPos;
                }
            }
        }

        #endregion

    }
}
