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

       public AudioSource[] allas;
        public GAMESTATE CurrentGameState { private set; get; }

       [SerializeField] int levelNo = 0;
        public event Action<GAMESTATE> OnGameStateChangedAction;
        #endregion

        #region Builtin Methods

        void Awake()
        {
            playerTankHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<TankHealth>();
            enemyHealths = FindObjectsOfType<TankHealth>().ToList();
            enemyHealths.Remove(playerTankHealth);
            enemyCount = enemyHealths.Count;
            levelNo = int.Parse(SceneManager.GetActiveScene().name);
            allas = FindObjectsOfType<AudioSource>() ;
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
                foreach(AudioSource ad in allas)
                {
                    ad.enabled = false;
                }
            }
            else if( gs == GAMESTATE.play)
            {
                Time.timeScale = 1f;
                foreach (AudioSource ad in allas)
                {
                    ad.enabled = true;

                }
            }
            else if (gs == GAMESTATE.dialogue)
            {
                Time.timeScale = 0f;
                foreach (AudioSource ad in allas)
                {
                    ad.enabled = false;
                }
            }
            else if(gs == GAMESTATE.levelComplete)
            {
                if (PlayerPrefs.GetInt("LEVEL", 1) <= levelNo) // 2 
                {
                    //set+=1
                    PlayerPrefs.SetInt("LEVEL", PlayerPrefs.GetInt("LEVEL", 1) + 1);
                }
            }
        }

        //Scene Management
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void GoToNextLevel()
        {
            if(Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
        public void GoToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("menu");
        }
        #endregion

    }
}
