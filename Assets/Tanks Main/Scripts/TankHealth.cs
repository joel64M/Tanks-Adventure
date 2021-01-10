using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
namespace NameSpaceName {

    public class TankHealth : MonoBehaviour , IDamagable
    {

        #region Variables
        public float startingHealth = 100f;
        float currentHealth = 0;
    
        bool isDead;
        //delegtes
        public event Action<float,float> HealthChangedAction;
        public event Action TankDestroyedAction;

        public Transform explosionTransform;
         ParticleSystem tankExplosionParticles;
        public AudioSource explosionAudioSource;
        public Transform explosionDecalTransform;
        #endregion

        #region Builtin Methods

        void Awake()
        {
            isDead = false;
            InitTankExplosion();
        }
    
        void OnEnable()
        {
            currentHealth = startingHealth;
            tankExplosionParticles.transform.parent = this.transform;
            tankExplosionParticles.transform.localPosition = Vector3.zero;
        }

        


        #endregion

        #region Custom Methods
        public  void SetTankHealth(float startHealth)
        {
            currentHealth = startingHealth = startHealth;
        }
        public void ResestHealth()
        {
			gameObject.SetActive(true);
			currentHealth = startingHealth;
			HealthChangedAction?.Invoke(currentHealth, startingHealth);
			isDead = false;
            tankExplosionParticles.transform.gameObject.SetActive(false);
            tankExplosionParticles.transform.SetParent(this.gameObject.transform);// = this.gameObject;
            explosionDecalTransform.transform.SetParent(this.gameObject.transform);


		}
		public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0 && !isDead)
			{
				OnDeath();
				TankDestroyedAction?.Invoke();
            }
            HealthChangedAction?.Invoke(currentHealth, startingHealth);
        }

        void InitTankExplosion()
        {
            tankExplosionParticles = explosionTransform.GetComponent<ParticleSystem>();
            explosionAudioSource = explosionTransform.GetComponent<AudioSource>();
 
            tankExplosionParticles.gameObject.SetActive(false);
        }

        void OnDeath()
        {

			isDead = true;
            tankExplosionParticles.transform.parent = null;
            tankExplosionParticles.gameObject.SetActive(true);
            tankExplosionParticles.Play();
            explosionDecalTransform.gameObject.SetActive(true);
            explosionDecalTransform.transform.parent = null;
            explosionAudioSource.Play();
            gameObject.SetActive(false);
        }
        #endregion

    }
}
