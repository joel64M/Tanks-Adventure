using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace NameSpaceName {

    public class SettingsUI : MonoBehaviour
    {

        #region Variables
        [SerializeField] Slider resoSlider;
        [SerializeField] Slider shadowDistanceSlider;
        [SerializeField] Slider msaaSlider;
        [SerializeField] SettingProfile settingsProfile;

        #endregion

        #region Builtin Methods

        void Start()
        {
            resoSlider.value = settingsProfile.resolutionPercentage;
            shadowDistanceSlider.value = settingsProfile.shadowDistance;
            msaaSlider.value = settingsProfile.antiAliasing;

        }

        #endregion

        #region Custom Methods
        public void ScreenResoSliderChanged(TextMeshProUGUI txt )
        {
            settingsProfile.resolutionPercentage =(int)resoSlider.value;
          //  print(resoSlider.value);
            txt.text = ((int)resoSlider.value).ToString() + "%";
            SettingsScript.instance.SetResolution(100);

            SettingsScript.instance.SetResolution((int)resoSlider.value);
        }
        public void ShadowDistanceSliderChanged(TextMeshProUGUI txt)
        {
            settingsProfile.shadowDistance = (int)shadowDistanceSlider.value;
            SettingsScript.instance.urp.shadowDistance = (int)shadowDistanceSlider.value;
            txt.text = ((int)shadowDistanceSlider.value).ToString() ;

        }
        public void MsaaSliderChanged(TextMeshProUGUI txt)
        {
            settingsProfile.antiAliasing = (int)msaaSlider.value;
            SettingsScript.instance.urp.msaaSampleCount = (int)msaaSlider.value * 2;
            txt.text = ((int)msaaSlider.value * 2).ToString() + "x";

        }
        #endregion

    }
}
