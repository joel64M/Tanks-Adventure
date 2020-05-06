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
     //   public float explosionForce = 1000f;
       // public float maxLifeTime = 2f;
       // public float explosionRadius = 5f;

        MeshRenderer mr;

        #endregion

        // Start is called before the first frame update

        private void OnEnable()
        {
            mr = GetComponent<MeshRenderer>();
            mr.enabled = true;
            explosionParticles.Play();

        }

        void Start()
        {
            //   Destroy(gameObject, maxLifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {

            /*

             Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);
             for (int i = 0; i < colliders.Length; i++)
             {

                 Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
                 if (targetRigidbody.GetComponent<ShellExplosion>())
                 {
                     continue;
                 }
                 if (!targetRigidbody)
                 {
                     continue;
                 }
                // targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

                 // Find the TankHealth script associated with the rigidbody.
 //                TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
                // if (!targetHealth)
                 {
                  //   continue;
                 }
                 float damage = CalculateDamage(targetRigidbody.position);

                 // Deal this damage to the tank.
               //  targetHealth.TakeDamage(damage);


             }
               */

            // Unparent the particles from the shell.
            //  explosionParticles.transform.parent = null;

            // Play the particle system.
            explosionParticles.Play();

            // Play the explosion sound effect.
            explosionAudio.Play();

            // Once the particles have finished, destroy the gameobject they are on.
            Invoke("HideShell", explosionParticles.main.duration);
            mr.enabled = false;
            if (other.gameObject.GetComponent<TankHealth>())
            {
                other.gameObject.GetComponent<IDamagable>().TakeDamage(maxDamage);
            }
            // Destroy the shell.dd
          //  Destroy(gameObject);
        }
        void HideShell()
        {
            gameObject.SetActive(false);
        }

        /*
        float CalculateDamage(Vector3 targetPosition)
        {
            // Create a vector from the shell to the target.
            Vector3 explosionToTarget = targetPosition - transform.position;

            // Calculate the distance from the shell to the target.
            float explosionDistance = explosionToTarget.magnitude;

            // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
            float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

            // Calculate damage as this proportion of the maximum possible damage.
            float damage = relativeDistance * maxDamage;

            // Make sure that the minimum damage is always 0.
            damage = Mathf.Max(0f, damage);

            return damage;
        }
        */
    }
}