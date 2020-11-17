using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Facebook.Unity;
using UnityEngine.Analytics;
namespace NameSpaceName
{

    public class SdkScript : MonoBehaviour
    {
        public static SdkScript instance;

        #region Variables
        int currentLevel;
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
                if (!FB.IsInitialized)
                {
                    FB.Init(InitCallback);
                }
                Debug.Log("starting from  " + PlayerPrefs.GetInt("LEVEL").ToString());
               
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
       
        private void OnLevelWasLoaded(int level)
        {
            if (instance == this)
            {
                if(level == 0)
                {

                }
                currentLevel = int.Parse(SceneManager.GetActiveScene().name);
            }
        }
    
   

        #endregion

        #region Custom Methods
        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            OnGameStartupUA(PlayerPrefs.GetInt("LEVEL"));
            LogGameStartupEventFB(PlayerPrefs.GetInt("LEVEL"));

        }
        public void OnApplicationPause(bool pause)
        {
            //   Debug.Log("pausing from " + PlayerPrefs.GetInt("LEVEL").ToString());
            OnGamePausedUA(currentLevel);

            if (!pause)
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                    LogGamePausedEventFB(currentLevel);
                }
              
            }
         
        }
        private void OnApplicationQuit()
        {
            //  Debug.Log("quitting from " + PlayerPrefs.GetInt("LEVEL").ToString());
            LogGameExitedEventFB(currentLevel);
            OnGameExitedtUA(currentLevel);
        }



        #region facebook funnels events
        public void LogLevelStartedEventFB(int levelNumber)
        {
            var parameters = new Dictionary<string, object>();
            double valToSum = (double)levelNumber;
            parameters["LevelNumber"] = levelNumber;
            FB.LogAppEvent(
                "Level Start1",
                (float)valToSum,
                parameters
            );
        }
        public void LogLevelCompleteEventFB(int levelNumber)
        {
            var parameters = new Dictionary<string, object>();
            double valToSum = (double)levelNumber;
            parameters["LevelNumber"] = levelNumber;
            FB.LogAppEvent(
                "Level Complete1",
                (float)valToSum,
                parameters
            );
        }
       public void LogLevelFailedEventFB(int levelNumber)
        {
            var parameters = new Dictionary<string, object>();
            double valToSum = (double)levelNumber;
            parameters["LevelNumber"] = levelNumber;
            FB.LogAppEvent(
                "Level Failed1",
                (float)valToSum,
                parameters
            );
        }
        public void LogLevelRestartedEventFB(int levelNumber)
        {
            var parameters = new Dictionary<string, object>();
            double valToSum = (double)levelNumber;
            parameters["LevelNumber"] = levelNumber;
            FB.LogAppEvent(
                "Level Restarted1",
                (float)valToSum,
                parameters
            );
        }
        public void LogGameExitedEventFB(int levelNumber)
        {
            var parameters = new Dictionary<string, object>();
            double valToSum = (double)levelNumber;
            parameters["LevelNumber"] = levelNumber;
            FB.LogAppEvent(
                "Game Exited1",
                (float)valToSum,
                parameters
            );
        }
        public void LogGamePausedEventFB(int levelNumber)
        {
            var parameters = new Dictionary<string, object>();
            double valToSum = (double)levelNumber;
            parameters["LevelNumber"] = levelNumber;
            FB.LogAppEvent(
                "Game Paused1",
                (float)valToSum,
                parameters
            );
        }
        public void LogGameStartupEventFB(int levelNumber)
        {
            var parameters = new Dictionary<string, object>();
            double valToSum = (double)levelNumber;
            parameters["LevelNumber"] = levelNumber;
            FB.LogAppEvent(
                "Game Startup1",
                (float)valToSum,
                parameters
            );
        }
        #endregion

        public void OnGameOverUA(int level)
        {
        
            Analytics.CustomEvent("gameOver1", new Dictionary<string, object>
            {
                {"Level ", level}

            });
        }
        public void OnGameCompleteUA( int level)
        {

            Analytics.CustomEvent("gameComplete1", new Dictionary<string, object>
            {
               {"Level ", level}

            });
        }
        public void OnGameStartUA(int level)
        {

            Analytics.CustomEvent("gameStart1", new Dictionary<string, object>
            {
                 {"Level ", level}

            });
        }
        public void OnGameRestartUA( int level)
        {

            Analytics.CustomEvent("gameRestart1", new Dictionary<string, object>
            {
                 {"Level ", level}

            });
        }
        public void OnGameExitedtUA(int level)
        {

            Analytics.CustomEvent("gameExited1", new Dictionary<string, object>
            {
                 {"Level ", level}

            });
        }
        public void OnGamePausedUA(int level)
        {

            Analytics.CustomEvent("gamePaused1", new Dictionary<string, object>
            {
                 {"Level ", level}

            });
        }
        public void OnGameStartupUA(int level)
        {

            Analytics.CustomEvent("gameStartup1", new Dictionary<string, object>
            {
                 {"Level ", level}

            });
        }
        #endregion

    }
}
