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

        [Header("Explosion Components")]
        public Transform explosionTransform;
         ParticleSystem tankExplosionParticles;
         AudioSource explosionAudioSource;
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

        void Start()
        {
            
        }

        void Update()
        {
           
        }

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



        #endregion

        #region Custom Methods
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                TankDestroyedAction?.Invoke();
                OnDeath();
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
            explosionAudioSource.Play();
            gameObject.SetActive(false);
        }
        #endregion

    }
}
