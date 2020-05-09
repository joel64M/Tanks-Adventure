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

        public Conversation currentConversation;
        int index;
        GameManager gm;
        public float typingSpeed = 1f;
        #endregion

        #region Builtin Methods

        void Awake()
        {
            gm = FindObjectOfType<GameManager>();
        }
        private void Start()
        {
            this.gameObject.SetActive(false);

        }
        private void OnEnable()
        {
            gm.SetGameState(GAMESTATE.dialogue);
            SetContinueButton(false);
        }
        private void OnDisable()
        {
            GetComponent<Canvas>().enabled = false;

        }
        #endregion

        #region Custom Methods
        public void _SkipButton()
        {
            print("Skipped");
            this.gameObject.SetActive(false);
            gm.SetGameState(GAMESTATE.play);

        }
        public void _ContinueButton()
        {
            print("Continue");
            SetContinueButton(false);
            NextLine();
        }
        public void InitializeConvo(Conversation convo)
        {
            index = 0;
            GetComponent<Canvas>().enabled = true;

            currentConversation = convo;
            this.gameObject.SetActive(true);
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
                print("End Convo");
                gm.SetGameState(GAMESTATE.play);
                this.gameObject.SetActive(false);
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
