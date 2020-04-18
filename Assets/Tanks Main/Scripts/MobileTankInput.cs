using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
namespace NameSpaceName {

    public class MobileTankInput : TankInput
    {

        #region Variables
      

        //temporary
        public bool isfiree;

        //components
        VariableJoystick joystick;

        [Header("Touch Struct")]
        [SerializeField] List<TouchhStruct> TouchStructList = new List<TouchhStruct>();
        [System.Serializable] struct TouchhStruct { public bool ignoreIt; public Touch touch; }
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

        /*
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
     */

        protected override void HandleMovementInputs()
        {
            // base.HandleMovementInputs();
            if (joystick)
            {
                VerticalInputValue = joystick.Vertical;
                HorizontalInputValue = joystick.Horizontal;
            }
        }
     
        protected override void HandleFireInputs()
        {

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    TouchhStruct t = new TouchhStruct();
                    t.ignoreIt = false;
                    t.touch = Input.GetTouch(i);
                    if (!IsPointerOverUIObject(Input.GetTouch(i),i))
                    {
                        t.ignoreIt = false;
                    }
                    else //if (IsPointerOverUIObject(Input.GetTouch(i),i))
                    {
                        t.ignoreIt = true;
                    }
                    
                            if(TouchStructList.Count>0)
                            {
                                 /*
                                if (!kkk.Any(dd => dd.touch.fingerId == Input.GetTouch(i).fingerId))
                                {
                                    kkk.Add(t);
                                }
                                */
                                bool isThere=false;
                                foreach (var item in TouchStructList)
                                {
                                    if ( item.touch.fingerId == Input.GetTouch(i).fingerId)
                                    {
                                    isThere = true;
                                    }

                                }
                                if (!isThere)
                                {
                                    TouchStructList.Add(t);
                                }
                            }
                            else
                            {
                                TouchStructList.Add(t);
                            }
                        if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved)
                        {
                           /*
                                if(!kkk.Any(oi => (oi.ignoreIt == true) && oi.touch.fingerId == Input.GetTouch(i).fingerId))
                                {
                                   // IsFire = true;
                                     // FireClick(Input.GetTouch(i).position);
                                    isfiree = true;
                                }
                            */
                             bool isThere = false;
                            foreach (var item in TouchStructList)
                            {
                                if (item.ignoreIt == false && item.touch.fingerId == Input.GetTouch(i).fingerId)
                                {
                                isThere = true;
                                }
                            }
                            if (isThere)
                            {
                            FindFirePos(Input.GetTouch(i).position);
                            isfiree = true;
                            }
                        }

                        if (Input.GetTouch(i).phase == TouchPhase.Ended || Input.GetTouch(i).phase == TouchPhase.Canceled)
                        {
                            if (!TouchStructList.Any(oi => (oi.ignoreIt == true) && oi.touch.fingerId == Input.GetTouch(i).fingerId))
                            {
                                IsFire = false;
                                isfiree = false;
                            }
                            TouchStructList.RemoveAll(oi => oi.touch.fingerId == Input.GetTouch(i).fingerId);
                        }
                }
            }
        }


        protected override void FindFirePos  (Vector2 mousePos)
        {
                //  base.FindFirePos();
                Vector3 hitPos;
                Ray ray = cam.ScreenPointToRay(mousePos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    //    if ((int)Mathf.Pow(2, hit.transform.gameObject.layer) == (int)layermask)
                    {
                        hitPos = hit.point;
                        hitPos.y = 0f;
                        FirePos = hitPos;
                    }
                }
        }

        private bool IsPointerOverUIObject(Touch touch,int i)
        {
            bool boo= false;
            if ( TouchStructList.Count>0 && i <TouchStructList.Count)
            {
                foreach (var item in TouchStructList)
                {
                    if(item.ignoreIt == false && item.touch.fingerId == touch.fingerId)
                    {
                        boo =  false;
                    }
                    else
                    {
                        boo =  true;
                    }
                }
                return boo;
                     
                /*
                if (!kkk.Any(oi => (oi.ignoreIt == true) && oi.touch.fingerId == touch.fingerId))
                {
                    return false;
                }
                else
                {
                    return true;
                }
                */
               
            }
            else
            {

                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
                {
                    return true;
                }
                else
                {
                    return false;
                }

                 //   PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                //    eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);
                //    List<RaycastResult> results = new List<RaycastResult>();
                //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
                //    return results.Count > 0;
            }
         
        }
        #endregion

    }
}
