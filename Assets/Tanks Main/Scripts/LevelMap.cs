using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    public class LevelMap : MonoBehaviour
    {

        #region Variables
        public   GameObject temp;

        public Transform parent;


        public static bool isSpawned;
      

        //public GameObject[] groundPrefabs;
        //public GameObject[] sidesPrefabs;
        //public GameObject[] toppingsPrefabs;
        //public GameObject[] treesPrrefabs;

        [System.Serializable]
        public class prefabClass
        {
            public string name;
            public GameObject[] prefab;
        }

        [Header("Prefabs")]
        public List<prefabClass> pList = new List<prefabClass>();
        #endregion

        #region Builtin Methods

        void Awake()
        {

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
