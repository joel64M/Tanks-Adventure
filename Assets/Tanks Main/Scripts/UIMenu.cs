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

    #endregion

    #region Custom Methods

        public void _LoadLevel()
        {
            print(EventSystem.current.currentSelectedGameObject.name);

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
