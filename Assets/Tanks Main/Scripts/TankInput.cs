using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    public class TankInput : MonoBehaviour
    {

        #region Variables
      //public exposed
        public LayerMask ignoreLayer;

        
        public bool IsFire
        {
            private set;
            get;
        }
        public Vector3 FirePos
        {
            private set;
            get;
        }

        //comopnents
        Camera cam;
        Vector3 hitPos;
        #endregion

        #region Builtin Methods

        void Awake()
        {
            cam = Camera.main;

        }

        void OnEnable()
        {

        }

        void Start()
        {
            
        }

        void Update()
        {
            GetWeaponInput();
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
        protected virtual void GetWeaponInput()
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
                     //   GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                      //  go.transform.position = hitPos;
                    }

                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                IsFire = false;
            }
        }

    #endregion

    }
}
