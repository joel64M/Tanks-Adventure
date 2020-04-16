using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    public class TankWeapon : MonoBehaviour
    {

        #region Variables
         public LayerMask layermask ;

        Camera cam;
        TankInput tankInputScript;
        float missileFireRate = 1f;
        bool canFire = true;

    #endregion

    #region Builtin Methods

        void Awake()
        {
        }

        void OnEnable()
        {

        }

        void Start()
        {
            tankInputScript = GetComponent<TankInput>();
        }

        void Update()
        {
            
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
        public void FireMissile(Vector3 mousePos)
        {
            if (canFire)
            {
                canFire = false;
                Invoke("ResetMissileFire", missileFireRate);
             

                // fire boom
                //get hit world pos
                Ray ray = cam.ScreenPointToRay(mousePos);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit, 100f))
                {
               //    if ((int)Mathf.Pow(2, hit.transform.gameObject.layer) == (int)layermask)
                    {
                        Vector3 hitPos = hit.point;
                         hitPos.y = 0f;
                        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        go.transform.position = hitPos;
                    }
                      
                }
            }
        }
       void ResetMissileFire()
        {
            canFire = true;
        }
       
    #endregion

    }
}
