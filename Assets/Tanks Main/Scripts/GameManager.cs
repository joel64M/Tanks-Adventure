using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
namespace NameSpaceName {
  public enum GAMESTATE { paused,  play, dialogue, levelComplete, levelFailed }

    public class GameManager : MonoBehaviour
    {

        #region Variables
        public int enemyCount = 0;
     public   List<TankHealth> enemyHealths = new List<TankHealth>();
        TankHealth playerTankHealth;

        public GAMESTATE CurrentGameState { private set; get; }

        
        public event Action<GAMESTATE> OnGameStateChangedAction;
        #endregion

        #region Builtin Methods

        void Awake()
        {
            playerTankHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<TankHealth>();
            enemyHealths = FindObjectsOfType<TankHealth>().ToList();
            enemyHealths.Remove(playerTankHealth);
            enemyCount = enemyHealths.Count;
        }

        void OnEnable()
        {
            playerTankHealth.TankDestroyedAction += OnLevelFailed;
            OnGameStateChangedAction += OnGameStateChanged;
            foreach (var item in enemyHealths)
            {
                item.TankDestroyedAction += OnEnemyDestroyed;
            }
        }

        void Start()
        {
           SetGameState(GAMESTATE.play);

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
            playerTankHealth.TankDestroyedAction -= OnLevelFailed;
            OnGameStateChangedAction -= OnGameStateChanged;
            foreach (var item in enemyHealths)
            {
                item.TankDestroyedAction -= OnEnemyDestroyed;
            }
        }


    #endregion

    #region Custom Methods
     
        public void SetGameState(GAMESTATE gs)
        {
            OnGameStateChangedAction?.Invoke(gs);
        }
        void OnEnemyDestroyed()
        {
            enemyCount--;
            if (enemyCount <= 0)
            {
                OnLevelComplete();
            }
        }
        void OnLevelFailed()
        {
            Invoke("OnLevelFailedDelay", 1.5f);
        }
        void OnLevelFailedDelay()
        {
            SetGameState(GAMESTATE.levelFailed);
        }
        private void OnLevelComplete()
        {
            SetGameState(GAMESTATE.levelComplete);
        }
        private void OnGameStateChanged(GAMESTATE gs)
        {
            CurrentGameState = gs;
            if(gs == GAMESTATE.paused)
            {
                Time.timeScale = 0;
            }
            else if( gs == GAMESTATE.play)
            {
                Time.timeScale = 1f;
            }
            else if (gs == GAMESTATE.dialogue)
            {
                Time.timeScale = 0f;
            }
        }

        //Scene Management
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void GoToNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        public void GoToMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }
        #endregion

    }
}
