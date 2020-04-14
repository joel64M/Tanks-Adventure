using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace tankTutorial
{
    public class TankShooting : MonoBehaviour
    {
        #region Variables
        public int playerNumber = 1;
        public Rigidbody shell;
        public Transform fireTransform;
        public Slider aimSlider;
        public AudioSource shootingAudio;
        public AudioClip chargingClip;
        public AudioClip fireClip;
        public float minLaunchForce = 15f;
        public float maxLaunchForce = 30f;
        public float maxChargeTime = 0.75f;

        string fireButton;
        float currentLaunchForce;
        float chargeSpeed;
        bool fired;
        #endregion

        private void OnEnable()
        {
            currentLaunchForce = minLaunchForce;
            aimSlider.value = minLaunchForce;

        }
        private void Start()
        {
            fireButton = "Fire1";
            chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;

        }
        private void Update()
        {
            aimSlider.value = minLaunchForce;
            if (currentLaunchForce >= maxLaunchForce && !fired)
            {
                currentLaunchForce = maxLaunchForce;
                Fire();
            }
            else if (Input.GetButtonDown(fireButton))
            {
                fired = false;
                currentLaunchForce = minLaunchForce;
                shootingAudio.clip = chargingClip;
                shootingAudio.Play();
            }
            else if (Input.GetButton(fireButton) && !fired)
            {
                currentLaunchForce += chargeSpeed * Time.deltaTime;
                aimSlider.value = currentLaunchForce;
            }
            else if (Input.GetButtonUp(fireButton) && !fired)
            {
                Fire();
            }
        }

        void Fire()
        {
            fired = true;
            Rigidbody shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation);

            shellInstance.velocity = currentLaunchForce * fireTransform.forward;

            shootingAudio.clip = fireClip;
            shootingAudio.Play();
            currentLaunchForce = minLaunchForce;
        }
    }
}