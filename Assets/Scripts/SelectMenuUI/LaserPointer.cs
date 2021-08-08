using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

namespace SelectMenuUI
{
    public struct PointerEventArgs
    {
        public SteamVR_Input_Sources fromInputSource;
        public uint flags;
        public float distance;
        public Transform target;
    }

    public delegate void PointerEventHandler(object sender, PointerEventArgs e);

    public delegate void ProcessDragHandler(object sender, PointerEventData e);

    public class LaserPointer : MonoBehaviour
    {
        public SteamVR_Behaviour_Pose pose;
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");
        public float thickness = 0.002f;
        //public GameObject holder;
        public GameObject pointer;
        public GameObject reticle;

        public event PointerEventHandler PointerClick;
        public event ProcessDragHandler ProcessDrag;

        private const float distMost = 100f;

        public static Vector2 ScreenCenterPoint { get { return new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); } }

        public virtual void OnPointerClick(PointerEventArgs e)
        {
            if (PointerClick != null)
                PointerClick(this, e);
        }

        public virtual void OnProcessDrag(PointerEventData e)
        {
            if (ProcessDrag != null)
            {
                ProcessDrag(this, e);
            }
        }

        private void Update()
        {
            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit, 100f);

            if (!bHit)
            {
                pointer.SetActive(false);
                reticle.SetActive(false);
                return;
            }
            GameObject gameObject = hit.collider.gameObject;
            string tag = gameObject.tag;
           
            if (tag != "scrollview" && tag != "image" && tag != "scrollbar")
            {
                pointer.SetActive(false);
                reticle.SetActive(false);
                return;
            }
            
            if (hit.distance < distMost)
            {
                float dist = hit.distance;

                pointer.SetActive(true);
                reticle.SetActive(true);

                pointer.transform.localScale = new Vector3(thickness, thickness, dist);
                pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
                pointer.transform.localRotation = Quaternion.identity;

                reticle.transform.position = hit.point;
            }
            else
            {
                return;
            }

            if (tag == "image" && interactWithUI.GetStateUp(pose.inputSource))
            {
                PointerEventArgs argsClick = new PointerEventArgs();
                argsClick.fromInputSource = pose.inputSource;
                argsClick.distance = hit.distance;
                argsClick.flags = 0;
                argsClick.target = hit.transform;
                OnPointerClick(argsClick);
            }

            if(tag == "scrollbar" && interactWithUI.GetState(pose.inputSource))
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.pointerDrag = gameObject;
                pointerData.button = PointerEventData.InputButton.Left;
                Debug.Log(gameObject);
                pointerData.dragging = true;
                pointerData.position = ScreenCenterPoint;
                pointerData.pressPosition = reticle.transform.position;
                


                OnProcessDrag(pointerData);
            }
            
        }

    }
}
