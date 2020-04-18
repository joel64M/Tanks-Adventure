using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
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

        public bool isjOysetick;
        public float joyval;
        bool IsJoystickPressed()
        {
            if(Mathf.Abs( joystick.Horizontal)>0.01f || Mathf.Abs(joystick.Vertical) > 0.01f)
            {
                isjOysetick = true;
                return true;
            }
            else
            {
                isjOysetick = false;
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
       public int touchID;
        protected override void HandleFireInputs()
        {
            joyval = Mathf.Abs(joystick.Horizontal) + Mathf.Abs(joystick.Vertical);

            /*
           
           if (Input.touchCount> 0)
           {
                if (IsPointerOverUIObject())
                {
                    count++;
                }


                if (!IsJoystickPressed())
                {

                }
                if(Input.touchCount>0 && !IsJoystickPressed())
                {
                    touchID = 0;

                }else if (Input.touchCount==2 && IsJoystickPressed())
                {
                    touchID
                }

                if(!IsJoystickPressed())
              
                
               if (IsJoystickPressed() ) //Input.touchCount >1
                {

                    // fireTouch = Input.GetTouch(1);
                    if (!IsFire && touchID == 0)
                        touchID = 1;
                    else
                        touchID = 0;
                  //  FireClick();


               11111
               }

               if(!IsJoystickPressed())
               {
                    //   fireTouch = Input.GetTouch(0);

                    touchID = 0;
                  //  FireClick();
               }
               
                if (Input.touchCount > touchID)
                {
                    fireTouch = Input.GetTouch(touchID);
                    FireClick();
                }
             

            }
    */


            if (Input.touchCount > 0)
            {
              //  Touch[] TOUCHES = Input.touches;
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Toucher t = new Toucher();
                    t.ignoreIt = false;
                    //t.ID = Input.GetTouch(i).fingerId;
                    t.touch = Input.GetTouch(i);
                    print(i);
                    if (!IsPointerOverUIObject(Input.GetTouch(i).position))
                    {
                        t.ignoreIt = false;
                    }
                    else if (IsPointerOverUIObject(Input.GetTouch(i).position))
                    {
                        t.ignoreIt = true;
                    }
                  //  if(kkk.Count > 0)
                    {
                        if(kkk.Count>0)
                        {
                            if (!kkk.Any(dd => dd.touch.fingerId == Input.GetTouch(i).fingerId))
                            {
                                kkk.Add(t);

                            }
                        }
                        else
                        {
                            kkk.Add(t);
                        }
                        if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved)
                        {
                            if(!kkk.Any(oi => (oi.ignoreIt == true) && oi.touch.fingerId == Input.GetTouch(i).fingerId))
                            {
                                isfiree = true;

                            }
                            //if (!kkk[i].ignoreIt) 
                            {
                               // isfiree = true;
                            }
                        }

                        if (Input.GetTouch(i).phase == TouchPhase.Ended || Input.GetTouch(i).phase == TouchPhase.Canceled)
                        {
                            if (!kkk.Any(oi => (oi.ignoreIt == true) && oi.touch.fingerId == Input.GetTouch(i).fingerId))
                            {
                                IsFire = false;
                                isfiree = false;
                            }
                               // if (!kkk[i].ignoreIt)
                            {
                              //  IsFire = false;
                            //    isfiree = false;

                            }
                           // int j = i-1;
                            kkk.RemoveAll(oi => oi.touch.fingerId == Input.GetTouch(i).fingerId);
                            if (i > 1)
                            {
                               // kkk.Find(ol => ol.ID == j).ID = j;
                                //  kkk[(j)].ID = j;
                               // Toucher oo = kkk.Where(lol => (lol.ID == j))  as Toucher;
                              //  kkk[j].ID =
                            }
                        }


                    }
                 
                }
            //    FireClick();

            }
        }
        public bool isfiree;
     [SerializeField] List<Toucher> kkk = new List<Toucher>();
        [System.Serializable] class Toucher {public bool ignoreIt; public Touch touch; }

        void FireClick()
        {
            if (fireTouch.phase == TouchPhase.Began || fireTouch.phase == TouchPhase.Moved)
            {
                isfiree = true;
                Vector3 hitPos;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
            else if (fireTouch.phase == TouchPhase.Ended)
            {
                IsFire = false;
                isfiree = false;
            }
        }
        private bool IsPointerOverUIObject(Vector2 vec)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(vec.x, vec.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        #endregion

    }
}
