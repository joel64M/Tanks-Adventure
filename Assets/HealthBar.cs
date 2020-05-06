using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NameSpaceName {

    public class HealthBar : MonoBehaviour
    {

        #region Variables
       public Image fillImage;
        Slider healthSlider;
        TankHealth th;
        Camera cam;
    #endregion

    #region Builtin Methods

        void Awake()
        {
            th = GetComponentInParent<TankHealth>();
            healthSlider = GetComponentInChildren<Slider>();
            cam = Camera.main;
        }

        void OnEnable()
        {
            th.HealthChangedAction += SetHealthBar;
            healthSlider.value = healthSlider.maxValue;
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
            if(cam)
            transform.LookAt(cam.transform.position);
            transform.Rotate(0, 180, 0);
        }

        void OnDisable()
        {
            th.HealthChangedAction -= SetHealthBar;
        }

        void Destroy()
        {

        }

    #endregion

    #region Custom Methods
        void SetHealthBar(float healthPect , float maxHealth)
        {
            healthSlider.value = healthPect / maxHealth;
        }
    #endregion

    }
}
