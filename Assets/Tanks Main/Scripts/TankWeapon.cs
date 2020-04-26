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
         Transform missileParent;

        //audio
        [Header("Audio Properties")]
        [SerializeField] AudioSource fireAudio;
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
            CreateMissileParent();
            CreateMissilePool();
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
                GetMissileFromPool();
            }
        }

        void ResetMissileFireRate()
        {
            canFire = true;
        }

        void CreateMissileParent()
        {
            GameObject go = new GameObject("Missile Parent");
            go.transform.position = Vector3.zero;
            missileParent = go.transform;
        }

        [SerializeField] int missileAmount = 40;
        int missileIndex=0;
        List<GameObject> missilePool = new List<GameObject>();

        void GetMissileFromPool()
        {
            if (missilePool[missileIndex].activeSelf)
            {
                CreateSingleMissile();
                missileIndex = missileAmount;
                missileAmount++;
            }
            missilePool[missileIndex].transform.position = firePoint.position;
            missilePool[missileIndex].transform.rotation = Quaternion.LookRotation(tankInputScript.FirePos - transform.position);
            missilePool[missileIndex].SetActive(true);
            missileIndex++;
            if (missileIndex >= missileAmount)
            {
                missileIndex = 0;
            }
        }

        void CreateMissilePool()
        {
            for (int i = 0; i < missileAmount; i++)
            {
                CreateSingleMissile();
            }
        }
        void CreateSingleMissile()
        {
            // Instantiate(missileShell, firePoint.position, Quaternion.LookRotation(tankInputScript.FirePos - transform.position),missileParent);
            GameObject go = Instantiate(missileShell, Vector3.zero, Quaternion.identity, missileParent);
            go.SetActive(false);
            missilePool.Add(go);
        }
    #endregion

    }
}
