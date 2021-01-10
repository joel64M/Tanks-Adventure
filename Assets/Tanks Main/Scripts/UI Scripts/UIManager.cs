using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace NameSpaceName {

    public class UIManager : MonoBehaviour
    {

        #region Variables
        //scripts cache
        TankHealth playerTankHealth;
        GameManager gm;

        [Header("UI Properties")]
        public Slider healthSlider;
        public TextMeshProUGUI countdownText;

        [Header("Panels")]
        public GameObject gamePlayPanel;
        public GameObject levelFailedPanel;
        public GameObject levelCompletePanel;
        public GameObject pausePanel;
        public GameObject startButton;
        #endregion

        #region Builtin Methods

        void Awake()
        {
            playerTankHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<TankHealth>();
            healthSlider = GetComponentInChildren<Slider>(true);
            healthSlider.value = healthSlider.maxValue;
            gm = FindObjectOfType<GameManager>();
            if (playerTankHealth.gameObject.GetComponent<MobileTankInput>() != null)
            playerTankHealth.gameObject.GetComponent<MobileTankInput>().joystick = GetComponentInChildren<VariableJoystick>();
            gamePlayPanel.SetActive(false);
            levelCompletePanel.SetActive(false);
            levelFailedPanel.SetActive(false);
            pausePanel.SetActive(false);

        }
        private void Start()
        {
            countdownText.gameObject.SetActive(false);
            startButton.SetActive(false);
            gm.SetGameState(GAMESTATE.play);
        }
        void OnEnable()
        {
            playerTankHealth.HealthChangedAction += UpdateUI;
            gm.OnGameStateChangedAction += OnStateChanged;
        }

        void OnDisable()
        {
            playerTankHealth.HealthChangedAction -= UpdateUI;
            gm.OnGameStateChangedAction -= OnStateChanged;

        }
        #endregion

        #region Custom Methods
        //buttons
        public void _StartGame()
        {
            startButton.SetActive(false);
            StartCoroutine(Countdown());
        }
        public void _PauseButton()
        {
            gm.SetGameState(GAMESTATE.paused);
        }
        public void _ResumeButton()
        {
            gm.SetGameState(GAMESTATE.play);
        }
        public void _ReveiveAdButton()
        {
            //if (Advertisements.Instance.IsRewardVideoAvailable())
            {
                Advertisements.Instance.ShowRewardedVideo(VideoComplete);
            }
        }

        private void VideoComplete(bool completed)
        {
            if (completed)
            {
                _Respawn();

            }
            else
            {
                gm.SetGameState(GAMESTATE.levelFailed);
            }
        }

        public void _Respawn()
        {

            levelFailedPanel.SetActive(false);
            playerTankHealth.ResestHealth();

            startButton.SetActive(true);

        }
        public void _RestartButton()
        {
            gm.RestartLevel();
        }
        public void _NextLevelButton()
        {
            gm.GoToNextLevel();
        }
        public void _MainMenuButton()
        {
            gm.GoToMainMenu();
        }
        IEnumerator Countdown()
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = 3.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownText.text = (2).ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownText.text = (1).ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownText.text = (0).ToString();
            countdownText.gameObject.SetActive(false);
            gm.SetGameState(GAMESTATE.play);
        }

        void UpdateUI(float currentHealth,float maxHealth)
        {
            healthSlider.value = currentHealth / maxHealth;
        }

        void OnStateChanged(GAMESTATE state)
        {
            gamePlayPanel.SetActive(false);
            levelCompletePanel.SetActive(false);
            levelFailedPanel.SetActive(false);
            pausePanel.SetActive(false);
            switch (state)
            {
              
                case GAMESTATE.play:
                    gamePlayPanel.SetActive(true);
                    break;
                case GAMESTATE.levelFailed:
                    levelFailedPanel.SetActive(true);
                    break;
                case GAMESTATE.levelComplete:
                    levelCompletePanel.SetActive(true);
                    break;
                case GAMESTATE.paused:
                    pausePanel.SetActive(true);
                    break;
                case GAMESTATE.dialogue:
                    //pausePanel.SetActive(false);
                    break;
                default:
                    Debug.Log("something is wrong i can feel it ");
                    break;
            }
        }

        #endregion
    }
}
