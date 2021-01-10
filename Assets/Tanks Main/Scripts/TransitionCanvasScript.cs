using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace NameSpaceName {

    public class TransitionCanvasScript : MonoBehaviour
    {

        #region Variables
        public static TransitionCanvasScript instance;
        [SerializeField] Animator anim;
        [SerializeField] float transitionTime = 1f;
     #endregion

    #region Builtin Methods

        void Awake()
        {
            instance = this;
        }

     

        #endregion

        #region Custom Methods

        public void TransitionToNextScene(int level)
        {
            StartCoroutine(LoadLevel(level));
        }

        IEnumerator LoadLevel(int level)
        {
            Time.timeScale = 1f;
            anim.SetBool("NextScene", true);
             yield return new WaitForSeconds(transitionTime);
            AsyncOperation operation = SceneManager.LoadSceneAsync(level,LoadSceneMode.Single);
            while (!operation.isDone)
            {
                yield return null;
            }
            //SceneManager.LoadScene(level);
        }
        #endregion

    }
}
