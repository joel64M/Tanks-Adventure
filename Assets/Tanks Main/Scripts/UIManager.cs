using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NameSpaceName {

    public class UIManager : MonoBehaviour
    {

        #region Variables
        //scripts cache
        TankHealth playerTankHealth;
        GameManager gm;

        [Header("UI Properties")]
        public Slider healthSlider;


        [Header("Panels")]
        public GameObject gamePlayPanel;
        public GameObject levelFailedPanel;
        public GameObject levelCompletePanel;
        public GameObject pausePanel;
        #endregion

        #region Builtin Methods

        void Awake()
        {
            playerTankHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<TankHealth>();
            healthSlider = GetComponentInChildren<Slider>(true);
            healthSlider.value = healthSlider.maxValue;
            gm = FindObjectOfType<GameManager>();

            gamePlayPanel.SetActive(false);
            levelCompletePanel.SetActive(false);
            levelFailedPanel.SetActive(false);
            pausePanel.SetActive(false);

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
        public void _PauseButton()
        {
            gm.SetGameState(GAMESTATE.paused);
        }
        public void _ResumeButton()
        {
            gm.SetGameState(GAMESTATE.play);
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
                    Debug.Log("lol say what??");
                    break;
            }
        }

        #endregion
    }
}
