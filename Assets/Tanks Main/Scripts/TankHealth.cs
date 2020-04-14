using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace tankTutorial
{
    public class TankHealth : MonoBehaviour
    {
        #region Variables
        public float startingHealth = 100f;
        public Slider healthSlider;
        public Image fillImage;
        public Color fullHealthColor = Color.green;
        public Color zeroHealthColor = Color.red;
        public GameObject explosionPrefab;

        AudioSource explosionAudioSource;
        ParticleSystem explosionParticles;
        float currentHealth;
        bool isDead;
        #endregion

        private void Awake()
        {
            explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
            explosionAudioSource = explosionParticles.GetComponent<AudioSource>();
            explosionParticles.gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            currentHealth = startingHealth;
            isDead = false;
            SetHealthUI();

        }
        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            SetHealthUI();
            if (currentHealth <= 0f && !isDead)
            {
                OnDeath();
            }
        }
        void SetHealthUI()
        {
            healthSlider.value = currentHealth;
            fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
        }
        void OnDeath()
        {
            isDead = true;
            explosionParticles.transform.position = transform.position;
            explosionParticles.gameObject.SetActive(true);
            explosionParticles.Play();
            explosionAudioSource.Play();
            gameObject.SetActive(false);
        }
    }
}