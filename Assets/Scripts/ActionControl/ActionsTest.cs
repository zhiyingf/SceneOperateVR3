using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;//This references the namespace needed to access the VR input classes.

public class ActionsTest : MonoBehaviour
{
    public SteamVR_Input_Sources handType;//The type of hand(s) to poll for input. These are either All, Left or Right.
    public SteamVR_Action_Boolean teleportAction;//Reference to the Teleport action.
    public SteamVR_Action_Boolean grabAction;//Reference to the Grab action.

    // Update is called once per frame
    void Update()
    {
        if (GetTeleportDown())
        {
            print("Teleport " + handType);
        }
        if (GetGrab())
        {
            print("Grab " + handType);
        }
    }

    //Poll if the Teleport action was just activated and return true if this is the case.
    public bool GetTeleportDown()
    {
        return teleportAction.GetStateDown(handType);
    }

    //Poll if the Grab action is currently activated.
    public bool GetGrab()
    {
        return grabAction.GetState(handType);
    }

}
