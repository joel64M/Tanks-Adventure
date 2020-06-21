using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {
    [CreateAssetMenu(fileName = "setting", menuName = "scriptable/settings")]
    public class SettingProfile : ScriptableObject
    {

        #region Variables
        [Range(20, 100)]         public int resolutionPercentage = 75;
        [Range(0, 2)]         public int antiAliasing = 1;
        [Range(30, 200)]         public int shadowDistance = 140;
        #endregion

        #region Builtin Methods

        void Awake()
        {

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
