using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMesh : MonoBehaviour
{
    public SceneSDFArea scenesdfArea;

    public void UpdateSDFMeshOnce()
    {
        scenesdfArea.ExecuteOnClick();
    }

    public void UpdateSDFMeshAways()
    {
        scenesdfArea.toggleIsEditor();
    }
}
