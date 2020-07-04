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

        public GameObject explosionEffect;
        public GameObject shootingEffect;
        public GameObject muzzleEffect;
        public AudioSource explosionAudio;
        public AudioSource shootAudio;

        public float maxDamage = 10f;


      public  MeshRenderer mr;

        #endregion


        private void OnEnable()
        {
            mr = GetComponent<MeshRenderer>();
            mr.enabled = true;
            // explosionParticles.Play();
            if (muzzleEffect)
            {
                muzzleEffect.transform.parent = null;
                Invoke("ParentMuzzleBack", 2f);
            }
            if (shootingEffect)
            {
                shootingEffect.SetActive(true);
            }
            if (explosionEffect)
            {
                explosionEffect.SetActive(false);
            }
           
        }



        private void OnTriggerEnter(Collider other)
        {
            //  explosionParticles.Play();

            // explosionAudio.Play();
            if (shootingEffect)
            {
                shootingEffect.SetActive(false);
            }
            if (explosionEffect)
            {
                explosionEffect.SetActive(true);
            }
            Invoke("HideShell", 1f);
            mr.enabled = false;
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
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

        void ParentMuzzleBack()
        {
            muzzleEffect.transform.parent = this.transform;
            muzzleEffect.transform.localPosition = Vector3.zero;
        }
  
    }
}