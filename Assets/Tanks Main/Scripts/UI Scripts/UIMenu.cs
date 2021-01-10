using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
namespace NameSpaceName {

    public class UIMenu : MonoBehaviour
    {

        #region Variables
        [SerializeField] AnimationCurve animCurve;
        [SerializeField] float animSpeed = 0.5f;
        [SerializeField] Transform contentTransform;
        [SerializeField] List<Button> buttons;
        [SerializeField] GameObject GDPRObject;
        [SerializeField] Button okButton;
        [SerializeField] GameObject adsButton;
        [SerializeField] GameObject restoreButton;

        #endregion

        #region Builtin Methods

        void Awake()
        {
            if (!PlayerPrefs.HasKey("LEVEL"))
            {
                PlayerPrefs.SetInt("LEVEL", 1);
            }
            InitLevelPanel();
      
        }
        private void Start()
        {
            if (PlayerPrefs.GetInt("AdsRemoved", 0) == 1)
            {
                adsButton.SetActive(false);
                restoreButton.SetActive(false);
            }
            SetGdpr(GDPRObject);
            IAPManager.Instance.InitializeIAPManager(InitComplete);
        }

        private void InitComplete(IAPOperationStatus status, string message, List<StoreProduct> allProducts)
        {
            if (status == IAPOperationStatus.Success)
            {
                for(int i = 0; i < allProducts.Count; i++)
                {
                    if (allProducts[i].productName == ShopProductNames.RemoveAds.ToString())
                    {
                        if (allProducts[i].active == true)
                        {
                            Advertisements.Instance.RemoveAds(true);
                            adsButton.SetActive(false);
                        }
                    }
                }
            }
        }
        #endregion

        #region Custom Methods
        public void SetGdpr(GameObject go)
        {
            if (Advertisements.Instance.UserConsentWasSet() == false)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
                Advertisements.Instance.Initialize();
            }
        }

        public void YesGDPR()
        {
            Advertisements.Instance.SetUserConsent(true);
            Advertisements.Instance.Initialize();
            //setactive false;
            GDPRObject.SetActive(false);
        }
        public void NoGDPR()
        {
            Advertisements.Instance.SetUserConsent(false);
            Advertisements.Instance.Initialize();
            GDPRObject.SetActive(false);
            //setactive false;
        }
        public void ToogleValueChanged(Toggle boo)
        {
            Debug.Log(boo);
            okButton.interactable = boo.isOn;
        }
        public void _OkGDPRButton()
        {
            YesGDPR();
        }
        public void _RemoveAds()
        {
            IAPManager.Instance.BuyProduct(ShopProductNames.RemoveAds, BuyComplete);
        }
        public void _RestorePurchases()
        {
            IAPManager.Instance.RestorePurchases(ProductRestoredCallback);
        }
        private void ProductRestoredCallback(IAPOperationStatus status, string message, StoreProduct product)
        {
            if (status == IAPOperationStatus.Success)
            {
                //consumable products are not restored
                //non consumable Unlock Level 1 -> unlocks level 1 so we set the corresponding bool to true
                if (product.productName ==ShopProductNames.RemoveAds.ToString() )
                {
                    Advertisements.Instance.RemoveAds(true);
                    adsButton.SetActive(false);
                   
                    PlayerPrefs.SetInt("AdsRemoved", 1);
                    restoreButton.SetActive(false);
                }
                             //unlockLevel1 = true;
                //subscription has been bought so we set our subscription variable to true
              //  if (product.productName == "Subscription") subscription = true;
            }
            else
            {
                Debug.Log("Buy product failed: " + message);
            }
            //an error occurred in the buy process, log the message for more details
        }
        private void BuyComplete(IAPOperationStatus status, string message, StoreProduct product)
        {
            if (status == IAPOperationStatus.Success)
            {
                if (product.productName == ShopProductNames.RemoveAds.ToString())
                {
                    Advertisements.Instance.RemoveAds(true);
                    adsButton.SetActive(false);
                    PlayerPrefs.SetInt("AdsRemoved", 1);
                }
                else
                {
                    Debug.Log(message + "****");
                }
            }
        }

        public void _LoadCurrentLevel()
        {
            if (TransitionCanvasScript.instance != null)
            {
          
                if (Application.CanStreamedLevelBeLoaded(PlayerPrefs.GetInt("LEVEL").ToString()))
                {
                    TransitionCanvasScript.instance.TransitionToNextScene(PlayerPrefs.GetInt("LEVEL"));
                }
            }
            else
            {
                if (Application.CanStreamedLevelBeLoaded(PlayerPrefs.GetInt("LEVEL").ToString()))
                {
                    SceneManager.LoadScene(PlayerPrefs.GetInt("LEVEL").ToString());
                }
            }

         
        }

        public void _LoadLevel()
        {
            string level = (EventSystem.current.currentSelectedGameObject.name);

            if (Application.CanStreamedLevelBeLoaded(level))
            {
                if (TransitionCanvasScript.instance != null)
                {
                    TransitionCanvasScript.instance.TransitionToNextScene(Int32.Parse(level));
                }
                else
                {
                    SceneManager.LoadScene(level);
                }
               
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
           // SceneManager.LoadScene(level);

        }

        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
        void InitLevelPanel()
        {
            buttons = contentTransform.GetComponentsInChildren<Button>().ToList();
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
                buttons[i].gameObject.name = (i + 1).ToString();
            }
            for (int i = PlayerPrefs.GetInt("LEVEL", 1); i <buttons.Count; i++)
            {
                buttons[i].interactable = false;
            }
        }


        //tweeen

        public void TweenFromTop(GameObject go)
        {
            LeanTween.moveLocalY(go, 0, animSpeed).setEase(animCurve);
        }
        public void TweenToTop(GameObject go)
        {
            LeanTween.moveLocalY(go, 1000, animSpeed).setEase(animCurve);
        }
        public void TweenToBottom(GameObject go)
        {
            LeanTween.moveLocalY(go, -1000, animSpeed).setEase(animCurve);
        }
        public void TweenFromBottom(GameObject go)
        {
            LeanTween.moveLocalY(go, 0, animSpeed).setEase(animCurve);
        }
        #endregion

    }
}
