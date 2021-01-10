using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
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
            levelNo = int.Parse(SceneManager.GetActiveScene().name);
        
            playerTankHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<TankHealth>();
            enemyHealths = FindObjectsOfType<TankHealth>().ToList();
            enemyHealths.Remove(playerTankHealth);
            enemyCount = enemyHealths.Count;
            allas = FindObjectsOfType<AudioSource>() ;
            // NavMeshBuilder.BuildNavMesh();
            //Time.timeScale = 0;

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
            //  SetGameState(GAMESTATE.play);

            if (SdkScript.instance != null)
            {
                SdkScript.instance.OnGameStartUA(levelNo);
                SdkScript.instance.LogLevelStartedEventFB(levelNo);
            }
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

            if (SdkScript.instance != null)
            {
                SdkScript.instance.OnGameOverUA(levelNo);
                SdkScript.instance.LogLevelFailedEventFB(levelNo);
            }
     
        }
        void OnLevelFailedDelay()
        {
            Time.timeScale = 0;
            //("OnLevelFailedDelay");
            SetGameState(GAMESTATE.levelFailed);

        }
        private void OnLevelComplete()
        {
            Invoke("OnLevelCompleteDelay", 1.5f);
            if (SdkScript.instance != null)
            {
                SdkScript.instance.OnGameCompleteUA(levelNo);
                SdkScript.instance.LogLevelCompleteEventFB(levelNo);
            }
        }
        void OnLevelCompleteDelay()
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
           
            if (SdkScript.instance != null)
            {
                SdkScript.instance.OnGameRestartUA(levelNo);
               SdkScript.instance.LogLevelRestartedEventFB(levelNo);
            }
            if(TransitionCanvasScript.instance != null)
            {
                TransitionCanvasScript.instance.TransitionToNextScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    
        public void GoToNextLevel()
        {
            if (levelNo > 1 && levelNo % 3 == 0)
            {
                if (Advertisements.Instance.IsInterstitialAvailable())
                {
                    Advertisements.Instance.ShowInterstitial();
                }
            }
            if (TransitionCanvasScript.instance != null)
            {
                if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
                {
                    TransitionCanvasScript.instance.TransitionToNextScene(SceneManager.GetActiveScene().buildIndex+1);
                }
                else
                {
                   // SceneManager.LoadScene("Menu");
                    TransitionCanvasScript.instance.TransitionToNextScene(0);
                }
            }
            else
            {
                if (Application.CanStreamedLevelBeLoaded(SceneManager.GetActiveScene().buildIndex + 1))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene("Menu");
                }
            }
       
        }
        public void GoToMainMenu()
        {
            Time.timeScale = 1f;
            if (TransitionCanvasScript.instance != null)
            {
                TransitionCanvasScript.instance.TransitionToNextScene(0);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        #endregion

    }
}
