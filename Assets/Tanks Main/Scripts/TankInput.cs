using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace NameSpaceName {

    public class TankInput : MonoBehaviour
    {

        #region Variables
      //public exposed
        public LayerMask ignoreLayer;

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
        Camera cam;
        Vector3 hitPos;
        #endregion

        #region Builtin Methods

        protected virtual void  Awake()
        {
            cam = Camera.main;
        }
        void OnEnable()
        {

        }

        void Start()
        {
            
        }

      protected virtual  void Update()
        {
            if(cam)
            HandleFireInputs();
            HandleMovementInputs();
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
        protected virtual void HandleFireInputs()
        {
          
            if (Input.GetMouseButton(0))
            {
               
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    //    if ((int)Mathf.Pow(2, hit.transform.gameObject.layer) == (int)layermask)
                    {
                         hitPos = hit.point;
                        hitPos.y = 0f;
                        IsFire = true;
                        FirePos = hitPos;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                IsFire = false;
            }
        }

        protected  virtual void HandleMovementInputs()
        {
            VerticalInputValue = Input.GetAxis("Vertical");
            HorizontalInputValue = Input.GetAxis("Horizontal");

        }
        #endregion

    }
}
