using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NameSpaceName {

    public class MobileTankInput : TankInput
    {

        #region Variables
        public VariableJoystick joystick;

        Touch uiTouch;
        Touch fireTouch;

        #endregion

        #region Builtin Methods
   
        protected override void Awake()
        {
            base.Awake();
            joystick = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<VariableJoystick>();
        }

        protected override void Update()
        {
            base.Update();
           
          
        }

        #endregion

        #region Custom Methods

        public bool joystickPressed;

        bool IsJoystickPressed()
        {
            if(Mathf.Abs( joystick.Horizontal)>0.01f || Mathf.Abs(joystick.Vertical) > 0.01f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void HandleMovementInputs()
        {
            // base.HandleMovementInputs();
            if (joystick)
            {
                VerticalInputValue = joystick.Vertical;
                HorizontalInputValue = joystick.Horizontal;
            }
        }
        /*
      public  int touchX=0;
        bool lockuiTouch = false;
        int uiNo;
        int fireNo;
        bool lockfireTouch = false;
        void TouchSystem()
        {

           if (Input.touchCount > 0)
            {
                if (IsPointerOverUIObject() && !lockuiTouch)
                {
                    lockuiTouch = true;
                    uiTouch = Input.GetTouch(touchX);
                    uiNo = touchX;
                    touchX++;
                }
                if(lockuiTouch)
                uiTouch = Input.GetTouch(uiNo);

                if (lockuiTouch)
                {
                    if (uiTouch.phase == TouchPhase.Began || uiTouch.phase == TouchPhase.Moved)
                    {
                     //   print("hhgkj");
                    }
                    else if( uiTouch.phase == TouchPhase.Ended)
                    {
                      //  print("moh");
                        touchX--;
                        lockuiTouch = false;
                    }

                }
                if (Input.touchCount >= 1 || (Input.touchCount==2 && lockuiTouch ) )
                {
                    if (!IsPointerOverUIObject() && !lockfireTouch)
                    {
                        lockfireTouch = true;
                        fireTouch = Input.GetTouch(touchX);
                        fireNo = touchX;
                        touchX++;
                    }
                  
                }
                if (lockfireTouch)
                    fireTouch = Input.GetTouch(fireNo);
                if (lockfireTouch)
                if (fireTouch.phase == TouchPhase.Began || fireTouch.phase == TouchPhase.Moved)
                {
                        FireClick();
                }
                else if (fireTouch.phase == TouchPhase.Ended)
                {
                    touchX--;
                        IsFire = false;
                    lockfireTouch = false;
                }
            }
            
        
        
        }
        */
        protected override void HandleFireInputs()
        {

            /*
             if (IsPointerOverUIObject())
             {
                 uiTouch = Input.GetTouch(touchX);
                 touchX++;
             }
             if(uiTouch.phase == TouchPhase.Ended)
             {
                 touchX--;
             }

             if (Input.touchCount > 1)
             {
                 fireTouch = Input.GetTouch(touchX);
                 //if(fireTouch.phase == TouchPhase.Began || fireTouch.phase == TouchPhase.Moved)
                 {
                     //shoot
                  //   FireClick();
                 }
             }
             else
             {
                 if (!IsPointerOverUIObject())
                 {
                     //shoot
                   //  FireClick();
                 }
             }

             if (!IsPointerOverUIObject())
                 if(Input.touchCount>1)
             base.HandleFireInputs();
             */
        }

        void FireClick()
        { 
            //if (fireTouch.phase == TouchPhase.Began || fireTouch.phase == TouchPhase.Moved)
            {
                Vector3 hitPos;
                Ray ray = Camera.main.ScreenPointToRay(fireTouch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    //    if ((int)Mathf.Pow(2, hit.transform.gameObject.layer) == (int)layermask)
                    {
                        hitPos = hit.point;
                        hitPos.y = 0f;
                        IsFire = true;
                        FirePos = hitPos;
                    }
                }
            }
           // else
            {
               // IsFire = false;
            }
           
        }
        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        #endregion

    }
}
