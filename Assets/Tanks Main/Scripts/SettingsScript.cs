using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
namespace NameSpaceName {

    public class SettingsScript : MonoBehaviour
    {
        public static SettingsScript instance;

        #region Variables
        [SerializeField] int reso;
        [SerializeField] SettingProfile settings;
        public UniversalRenderPipelineAsset urp;
        int origHeight, origWidth;
        #endregion

        #region Builtin Methods

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
     
        void OnEnable()
        {

        }

        void Start()
        {
           // UniversalRenderPipelineAsset lightweightRenderPipelineAsset = GraphicsSettings.currentRenderPipeline  as UniversalRenderPipelineAsset ;
            urp.msaaSampleCount =settings.antiAliasing*2;
            urp.shadowDistance = settings.shadowDistance;
            origHeight = (int)Screen.currentResolution.height;
            origWidth = (int)Screen.currentResolution.width;


            SetResolution(settings.resolutionPercentage);
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
        
 public void SetResolution(int resolution)     {             reso = resolution;        int height = (int)(origHeight * resolution) / 100;       int width = (int)(origWidth * resolution) / 100;         //print(width);         //print(height);      Screen.SetResolution(width, height, true);     }
    #endregion

    }
}
