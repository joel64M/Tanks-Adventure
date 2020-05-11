using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    public class LookAtUi : MonoBehaviour
    {

        #region Variables
        Camera cam;

        #endregion

        #region Builtin Methods

        void Awake()
        {
            cam = Camera.main;

        }

        void OnEnable()
        {
            
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
            if (cam)
                transform.LookAt(cam.transform.position);
            transform.Rotate(0, 180, 0);
        }

        void OnDisable()
        {

        }

        void Destroy()
        {

        }

    #endregion

    #region Custom Methods

    #endregion

    }
}
