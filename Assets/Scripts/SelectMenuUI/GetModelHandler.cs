using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SelectMenuUI;
using UnityEngine.EventSystems;

public class GetModelHandler : MonoBehaviour
{
    public LaserPointer laserPointer;
    public Transform candidateModels;
    public Transform parentModel;

    void Awake()
    {
        laserPointer.PointerClick += PointerClick;
        laserPointer.ProcessDrag += ProcessDrag;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        GameObject model = candidateModels.Find(e.target.name).Find(e.target.name).gameObject;
        Instantiate(model, transform.position, transform.rotation, parentModel);
        //Rigidbody newModelRig = newModel.AddComponent<Rigidbody>();
        //newModelRig.useGravity = false;
        //MeshCollider newModelCol = newModel.AddComponent<MeshCollider>();
        //newModelCol.convex = true;
        //newModelCol.isTrigger = true;
    }

    public void ProcessDrag(object sender, PointerEventData eventData)
    {
        if (eventData.dragging && eventData.pointerDrag != null)
        {
            Debug.Log(eventData.pointerDrag+"!!!");
            ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.dragHandler);
        }
    }


}
