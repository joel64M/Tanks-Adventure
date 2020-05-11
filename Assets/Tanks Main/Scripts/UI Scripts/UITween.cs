using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NameSpaceName {

    public class UITween : MonoBehaviour
    {

        #region Variables
       [SerializeField] bool imageFillRepeat;
        [SerializeField] bool pingPong;

        [SerializeField] Image image;
        [SerializeField] float speed = 5f;
        float timer = 0;
    #endregion

    #region Builtin Methods

        void Awake()
        {
            if(pingPong)
            LeanTween.moveLocalX(gameObject, 40f, speed).setEaseLinear().setLoopPingPong();
        }

        void OnEnable()
        {

        }

        void Start()
        {
        }

        void Update()
        {
            if (imageFillRepeat)
            {
                timer += Time.deltaTime * speed;
                image.fillAmount = timer;
                if (timer >= 1)
                {
                    timer = 0f;
                }
            }
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
