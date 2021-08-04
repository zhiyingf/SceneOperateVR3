using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class ControllerGrabObject : MonoBehaviour
{
    //It stores  references to the hand type and the actions.
    public SteamVR_Input_Sources handType;
    //public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean grabAction;

    //Stores the GameObject that the trigger is currently colliding with, so you have the ability to grab the object.
    private GameObject collidingObject;
    //Serves as a reference to the GameObject that the player is currently grabbing.
    private GameObject objectInhand;


    //This method accepts a collider as a parameter and uses its GameObject as the collidingObject for grabbing and releasing.
    private void SetCollidingObject(Collider col)
    {
        //Doesn’t make the GameObject a potential grab target if the player is already holding something or the object has no rigidbody.
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        //Assigns the object as a potential grab target.
        collidingObject = col.gameObject;
    }

    private void ReleaseCollidingObject(Collider col)
    {
        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }


    //1 When the trigger collider enters another, this sets up the other collider as a potential grab target.
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    //2 Similar to section one (// 1), but different because it ensures that the target is set when the player holds a controller over an object for a while. Without this, the collision may fail or become buggy.???
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    //3 When the collider exits an object, abandoning an ungrabbed target, this code removes its target by setting it to null.
    public void OnTriggerExit(Collider other)
    {
        ReleaseCollidingObject(other);
    }

    //Make a new fixed joint,add it to the controller,and then set it up so it doesn't break easily.Finally ,you return it.
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
    private void GrabObject()
    {
        if (collidingObject)
        {
            //1 Move the colling GameObject into the player's hand and remove it from the collingObject variable.
            objectInhand = collidingObject;
            collidingObject = null;
            //2 Add a new joint that connects the controller to the object using the AddFixedJoint() method above.
            var joint = AddFixedJoint();
            joint.connectedBody = objectInhand.GetComponent<Rigidbody>();
        }
    }

    private void ReleaseObject()
    {
        //1 Make sure there’s a fixed joint attached to the controller.
        if (objectInhand && GetComponent<FixedJoint>())
        {
            //2 Remove the connection to the object held by the joint and destroy the joint.
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            //3 Add the speed and rotation of the controller when the player releases the object, so the result is a realistic arc.
            //objectInhand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            //objectInhand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        //4 Remove the reference to the formerly attached object.
        objectInhand = null;
    }

    // Update is called once per frame
    void Update()
    {
        //1 When the player triggers the Grab action, grab the object.
        if (grabAction.GetLastStateDown(handType))
        {
            GrabObject();
        }

        //2 If the player releases the input linked to the Grab action and there’s an object attached to the controller, this releases it.
        if (grabAction.GetLastStateUp(handType))
        {
            ReleaseObject();
        }

    }
}
