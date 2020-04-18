using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {
    [RequireComponent(typeof(TankInput))]
    public class TankWeapon : MonoBehaviour
    {

        #region Variables
        //public exposed
        [Header("Turret Propeties")]
        public Transform turret;
        public Transform firePoint;
        public float turretDegPerSecSpeed = 60f;
        //missile properties
        public float missileFireRate = 1f;
        public GameObject missileShell;
        public Transform missileParent;

        //audio
        [Header("Audio Properties")]
        public AudioSource fireAudio;
        public AudioClip fireClip;

        //private
        Vector3 finalTurretDirection;
        Vector3 turretDirectionVector;

        bool canFire = true;
        bool isTurretRotated = true;

        //components
        Camera cam;
        TankInput tankInputScript;

        #endregion

        #region Builtin Methods

        void Start()
        {
            tankInputScript = GetComponent<TankInput>();
            GameObject go = new GameObject("Missile Parent");
            go.transform.position = Vector3.zero;
            missileParent = go.transform;
        }

        void Update()
        {
            if (tankInputScript)
            {
                if (tankInputScript.IsFire)
                    isTurretRotated = false;
              //if (!isTurretRotated)
                    HandleTurret();
            }
        }

      
    #endregion

    #region Custom Methods
       
        void HandleTurret()
        {
            //calculate direction to the firepos
            turretDirectionVector = tankInputScript.FirePos- transform.position;
            turretDirectionVector.y = 0f;

            //convert to quaternion
            Quaternion rotation = Quaternion.LookRotation(turretDirectionVector);
            rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);

            turret.rotation = Quaternion.RotateTowards(turret.rotation, rotation,turretDegPerSecSpeed * Time.deltaTime);

            if(turret.forward.normalized == turretDirectionVector.normalized && !isTurretRotated)
            {
                isTurretRotated = true;
                FireMissile();
            }
            // finalTurretDirection = Vector3.MoveTowards(finalTurretDirection, turretDirectionVector, Time.deltaTime * turretTurnSpeed);
            // turret.rotation = Quaternion.LookRotation(finalTurretDirection);
            //turret.eulerAngles = Vector3.RotateTowards(turret.eulerAngles, turretDirectionVector, Time.deltaTime * turretTurnSpeed);
        }

        void FireMissile()
        {
            if (canFire)
            {
                canFire = false;
                Invoke("ResetMissileFireRate", missileFireRate);
                fireAudio.Play();
                Instantiate(missileShell, firePoint.position, Quaternion.LookRotation(tankInputScript.FirePos - transform.position),missileParent);
            }
        }

        void ResetMissileFireRate()
        {
            canFire = true;
        }
       
    #endregion

    }
}
