using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NameSpaceName {

    public class UIDialogue : MonoBehaviour
    {

        #region Variables
        [SerializeField] SpeakerUI leftSpeaker;
        [SerializeField] SpeakerUI rightSpeaker;
        [SerializeField] Button continueButton;
        [SerializeField] GameObject continueButtonFake;
        Canvas canvas;
        public Conversation currentConversation;
        int index;
        GameManager gm;
        public float typingSpeed = 1f;
        TriggerConversation tc;
        #endregion

        #region Builtin Methods

        void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            canvas = GetComponent<Canvas>();
            canvas.enabled = false;
        }
        private void Start()
        {
            //this.gameObject.SetActive(false);
        }
        private void OnEnable()
        {
           // gm.SetGameState(GAMESTATE.dialogue);
          //  SetContinueButton(false);
        }
        private void OnDisable()
        {
            //canvas.enabled = false;
        }

        #endregion

        #region Custom Methods
        public void InitializeConvo(Conversation convo,TriggerConversation trigC)
        {
            index = 0;
            tc = trigC;
            canvas.enabled = true;
            currentConversation = convo;
          //  this.gameObject.SetActive(true);
            gm.SetGameState(GAMESTATE.dialogue);
            SetContinueButton(false);
            NextLine();
        }

        public void _SkipButton()
        {
            //   tc.un
            canvas.enabled = false;
         //   this.gameObject.SetActive(false);
            gm.SetGameState(GAMESTATE.play);
            tc.UnlockCollision();

        }

        public void _ContinueButton()
        {
            SetContinueButton(false);
            NextLine();
        }
     
        void NextLine()
        {
            if (index < currentConversation.dialogueLines.Length )
            {
                DisplayLine();
                index++;
            }
            else
            {
                //  tc.Invoke("UnlockCollision", tc.timeToUnlockCollision);
                // gm.SetGameState(GAMESTATE.play);
                // this.gameObject.SetActive(false);
                _SkipButton();
            }
        }
        void DisplayLine()
        {
            if (currentConversation.dialogueLines[index].character == currentConversation.leftCharacter)
            {
                leftSpeaker.gameObject.SetActive(true);
                rightSpeaker.gameObject.SetActive(false);
                StartCoroutine(TypeLine(leftSpeaker));
            }
            else
            {
                leftSpeaker.gameObject.SetActive(false);
                rightSpeaker.gameObject.SetActive(true);
                StartCoroutine(TypeLine(rightSpeaker));
            }
        }

        IEnumerator TypeLine(SpeakerUI sui)
        {
            sui.dialogueTxt.text = "";
            sui.characterNameTxt.text = currentConversation.dialogueLines[index].character.charName;
            sui.characterImage.sprite = currentConversation.dialogueLines[index].character.charImage;
            foreach (var item in currentConversation.dialogueLines[index].characterDialogue)
            {
                sui.dialogueTxt.text += item;
                if (item == ' ')
                {
                    continue;
                }
                yield return new WaitForSecondsRealtime(typingSpeed);
            }
            SetContinueButton(true);
        }
        void SetContinueButton(bool boo)
        {
            continueButton.enabled = boo;
            continueButtonFake.SetActive(boo);
        }
        #endregion

    }
}
