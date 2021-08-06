using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class GetModelHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public Transform candidateModels;
    public Transform parentModel;

    void Awake()
    {
        //laserPointer.PointerIn += PointerInside;
        //laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        //Debug.Log(System.Environment.CurrentDirectory);
        Debug.Log(e.target.name + "--PointerClick");

        GameObject model = candidateModels.Find(e.target.name).Find(e.target.name).gameObject;
        GameObject newModel = Instantiate(model, transform.position, transform.rotation, parentModel);
        //Rigidbody newModelRig = newModel.AddComponent<Rigidbody>();
        //newModelRig.useGravity = false;
        //MeshCollider newModelCol = newModel.AddComponent<MeshCollider>();
        //newModelCol.convex = true;
        //newModelCol.isTrigger = true;
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        Debug.Log(e.target.name + "--PointerInside");
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        Debug.Log(e.target.name + "--PointerOutside");
    }


}
