using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    public class TriggerConversation : MonoBehaviour
    {

        #region Variables
        [SerializeField] Conversation convo;
        [SerializeField] GameObject dialogueCanvas;
         UIDialogue uiDialogue;
        bool lockCollision=false;
       public float timeToUnlockCollision = 5f;
        public bool completeLevelOnConvoEnd = false;
        #endregion

        #region Builtin Methods

        private void OnCollisionEnter(Collision other)
        {
            if (lockCollision)
            {
                return;
            }

            lockCollision = true;

            if (other.gameObject.CompareTag("Player"))
            {
                uiDialogue.InitializeConvo(convo,this);
            }
        }

        
        void Awake()
        {
            uiDialogue = FindObjectOfType<UIDialogue>();
            //sddsx = GameObject.FindGameObjectWithTag("DialogueCanvas");
            if (uiDialogue == null)
            {
                GameObject go = Instantiate(dialogueCanvas);
                //go.SetActive(false);
                uiDialogue = go.GetComponent<UIDialogue>();
            }
        }
        
        public void UnlockCollision()
        {
            if(completeLevelOnConvoEnd)
            FindObjectOfType<GameManager>().SetGameState(GAMESTATE.levelComplete);
            Invoke("Unlock", timeToUnlockCollision);
        }

        void Unlock()
        {
            lockCollision = false;

        }
        void OnEnable()
        {

        }

        void Start()
        {
            
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

        }

        void Destroy()
        {

        }

    #endregion

    #region Custom Methods

    #endregion

    }
}
