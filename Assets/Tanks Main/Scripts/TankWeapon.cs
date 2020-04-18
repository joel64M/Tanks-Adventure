﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {
    [RequireComponent(typeof(TankInput))]
    public class TankWeapon : MonoBehaviour
    {

        #region Variables
        //public exposed
        public Transform turret;
        public float turretDegPerSecSpeed = 60f;
        Vector3 finalTurretDirection;
        Vector3 turretDirectionVector;
        Camera cam;
        TankInput tankInputScript;
        public GameObject shell;
        public Transform firePoint;
        public float missileFireRate = 1f;
        bool canFire = true;
        bool isTurretRotated = true;

    #endregion

    #region Builtin Methods

        void Start()
        {
            tankInputScript = GetComponent<TankInput>();
        }

        void Update()
        {
            if (tankInputScript)
            {
                if (tankInputScript.IsFire)
                    isTurretRotated = false;
              //  if (!isTurretRotated)
                    HandleTurret();
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
       
        void HandleTurret()
        {
            turretDirectionVector = tankInputScript.FirePos- transform.position;
            turretDirectionVector.y = 0f;

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
                Invoke("ResetMissileFire", missileFireRate);
                Instantiate(shell, firePoint.position, Quaternion.LookRotation(tankInputScript.FirePos - transform.position));
            }
        }

        void ResetMissileFire()
        {
            canFire = true;
        }
       
    #endregion

    }
}
