using System.Collections;
using Valve.VR;
using UnityEngine;
using Unity.Profiling;

public class InputController : MonoBehaviour
{
    // HANDS
    [Header("SteamVR Controllers")]
    public SteamVR_Behaviour_Pose primaryHandObject;
    public SteamVR_Behaviour_Pose secondaryHandObject;

    // INPUTS
    [Header("SteamVR Actions")]
    public SteamVR_Action_Boolean unionAddAction;
    public SteamVR_Action_Boolean intersectionAddAction;
    public SteamVR_Action_Boolean subtractAddAction;
    public SteamVR_Action_Boolean delectAction;

    public SteamVR_Action_Boolean grabModelAction;
    public SteamVR_Action_Boolean interactUIAction;

    public SteamVR_Action_Boolean editorAction;//leftHand
    public SteamVR_Action_Boolean updateAction;//rightHand

    public SteamVR_Action_Boolean selectMenuAction;//select model by penal , menu
    public SteamVR_Action_Boolean saveMeshAction;//

    public SteamVR_Action_Pose pose;
    public SteamVR_Input_Sources primarySource = SteamVR_Input_Sources.RightHand;
    public SteamVR_Input_Sources secondarySource = SteamVR_Input_Sources.LeftHand;
    public Transform headTransform;

    public SteamVR_Action_Vibration hapticAction;

    // ACTION CONTROLLERS
    [Header("App controllers")]

    public GrabAddDelect grabAddDelectController;
    public SaveMeshController saveMeshController;
    public UpdateMesh updateMeshController;

    private Action currentAction = Action.Idle;
    private bool waitingForConfirm = false;

    static ProfilerMarker InputControllerMarker = new ProfilerMarker("SceneSDFArea");


    private enum Action
    {
        Idle,
        Select,
        Grab
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingForConfirm) return;

        InputControllerMarker.Begin();

        if (currentAction.Equals(Action.Idle))
        {
            //right hand
            if (grabModelAction.GetLastStateDown(primarySource))
            {
                currentAction = Action.Grab;
                grabAddDelectController.GrabObject(false);
            }
            else if (unionAddAction.GetStateDown(primarySource))
            {
                if (grabAddDelectController.AddOperation(BooleanType.Union, false))
                {
                    hapticAction.Execute(0f, 0.1f, 25, 5, primarySource);
                    Debug.Log("right unionAddAction!");
                }
            }
            else if (intersectionAddAction.GetStateDown(primarySource))
            {
                if (grabAddDelectController.AddOperation(BooleanType.Intersection, false))
                {
                    hapticAction.Execute(0f, 0.1f, 25, 5, primarySource);
                    Debug.Log("right intersectionAddAction!");
                }
            }
            else if (subtractAddAction.GetStateDown(primarySource))
            {
                if (grabAddDelectController.AddOperation(BooleanType.Subtract, false))
                {
                    hapticAction.Execute(0f, 0.1f, 25, 5, primarySource);
                    Debug.Log("right subtractAddAction!");
                }
            }
            else if (delectAction.GetStateDown(primarySource))
            {
                if (grabAddDelectController.DelectOperation(false))
                {
                    hapticAction.Execute(0f, 0.1f, 25, 5, primarySource);
                    Debug.Log("right delectAction!");
                }
            }
            else if (updateAction.GetStateDown(primarySource))
            {
                updateMeshController.UpdateSDFMeshOnce();
                Debug.Log("right updateAction!");
            }


            //left hand
            if (grabModelAction.GetLastStateDown(secondarySource))
            {
                currentAction = Action.Grab;
                grabAddDelectController.GrabObject(true);
            }
            else if (unionAddAction.GetStateDown(secondarySource))
            {
                if (grabAddDelectController.AddOperation(BooleanType.Union, true))
                {
                    hapticAction.Execute(0f, 0.1f, 25, 5, secondarySource);
                    Debug.Log("left unionAddAction!");
                }
            }
            else if (intersectionAddAction.GetStateDown(secondarySource))
            {
                if (grabAddDelectController.AddOperation(BooleanType.Intersection, true))
                {
                    hapticAction.Execute(0f, 0.1f, 25, 5, secondarySource);
                    Debug.Log("left intersectionAddAction!");
                }
            }
            else if (subtractAddAction.GetStateDown(secondarySource))
            {
                if (grabAddDelectController.AddOperation(BooleanType.Subtract, true))
                {
                    hapticAction.Execute(0f, 0.1f, 25, 5, secondarySource);
                    Debug.Log("left subtractAddAction!");
                }
            }
            else if (delectAction.GetStateDown(secondarySource))
            {
                if (grabAddDelectController.DelectOperation(true))
                {
                    hapticAction.Execute(0f, 0.1f, 25, 5, secondarySource);
                    Debug.Log("left delectAction!");
                }
            }
            else if (editorAction.GetStateDown(secondarySource))
            {
                updateMeshController.UpdateSDFMeshAways();
                Debug.Log("editorAction!");
            }
            else if (saveMeshAction.GetStateDown(secondarySource))
            {
                saveMeshController.ExportToOBJ();
                Debug.Log("saveMeshAction!");
            }
        }
        else if (currentAction.Equals(Action.Grab))
        {
            //right hand
            if (grabModelAction.GetLastStateUp(primarySource))
            {
                currentAction = Action.Idle;
                grabAddDelectController.ReleaseObject(false);
            }
            //left hand
            if (grabModelAction.GetLastStateUp(secondarySource))
            {
                currentAction = Action.Idle;
                grabAddDelectController.ReleaseObject(true);
            }
        }else if (currentAction.Equals(Action.Select))
        {

        }


        InputControllerMarker.End();

       


    }
}
