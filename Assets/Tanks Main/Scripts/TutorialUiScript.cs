using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    public class TutorialUiScript : MonoBehaviour
    {

        #region Variables
        GameManager gm;
        [SerializeField] GameObject joystickTutObj;
        [SerializeField] GameObject fireTutObj;

        [SerializeField] bool isLevel1;
        bool done;
        #endregion

        #region Builtin Methods
        private void Start()
        {
            gm = FindObjectOfType<GameManager>();
            joystickTutObj.SetActive(false);
            fireTutObj.SetActive(false);
        }

        private void Update()
        {
            if (gm.CurrentGameState == GAMESTATE.play && !done) 
            {

                done = true;
                if (isLevel1)
                {
                    StartCoroutine(SetActiveAfter(joystickTutObj));
                }
                else
                {
                    StartCoroutine(SetActiveAfter(fireTutObj));
                }
            }
           if (gm.CurrentGameState == GAMESTATE.levelComplete || gm.CurrentGameState == GAMESTATE.paused || gm.CurrentGameState == GAMESTATE.levelFailed || gm.CurrentGameState == GAMESTATE.dialogue)
            {
                joystickTutObj.SetActive(false);
                fireTutObj.SetActive(false);
            }
        }

        #endregion

        #region Custom Methods
        IEnumerator SetActiveAfter(GameObject go)
        {
            yield return new WaitForSecondsRealtime(2.5f);
            go.SetActive(true);
        }
        #endregion

    }
}
