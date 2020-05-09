using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName
{
    public class ShellExplosion : MonoBehaviour
    {

        #region Varibles
        public LayerMask tankMask;
        public ParticleSystem explosionParticles;
        public AudioSource explosionAudio;
        public float maxDamage = 10f;


        MeshRenderer mr;

        #endregion


        private void OnEnable()
        {
            mr = GetComponent<MeshRenderer>();
            mr.enabled = true;
            explosionParticles.Play();
        }



        private void OnTriggerEnter(Collider other)
        {

       
            explosionParticles.Play();

            explosionAudio.Play();

            Invoke("HideShell", explosionParticles.main.duration);
            mr.enabled = false;
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (other.GetComponent<IDamagable>() != null)
                {
                    other.GetComponent<IDamagable>().TakeDamage(maxDamage);
                }
            }
           
      
        }
        void HideShell()
        {
            gameObject.SetActive(false);
        }

  
    }
}