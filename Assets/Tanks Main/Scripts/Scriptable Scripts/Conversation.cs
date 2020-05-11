using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    [System.Serializable]
    public struct Line
    {
        public Character character;
        [TextArea(1,4)]
        public string characterDialogue;
    }

    [CreateAssetMenu(fileName ="Convo",menuName ="Conversation")]
    public class Conversation : ScriptableObject
    {

        #region Variables
        public Character leftCharacter;
        public Character rightCharacter;
        public Line[] dialogueLines ;
        #endregion


    }
}
