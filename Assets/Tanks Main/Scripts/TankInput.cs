using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    public class TankInput : MonoBehaviour
    {

        #region Variables
        //comopnents
        TankWeapon tankWeaponScript;
        Camera cam;


        //
        public bool IsFire
        {
            private set;
            get;
        }
        public Vector3 ShootPos
        {
            private set;
            get;
        }
    #endregion

    #region Builtin Methods

        void Awake()
        {
            tankWeaponScript = GetComponent<TankWeapon>();
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
                //  tankWeaponScript.FireMissile(Input.mousePosition);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    //    if ((int)Mathf.Pow(2, hit.transform.gameObject.layer) == (int)layermask)
                    {
                        Vector3 hitPos = hit.point;
                        hitPos.y = 0f;
                     //   GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                      //  go.transform.position = hitPos;
                    }

                }
            }
        }

    #endregion

    }
}
