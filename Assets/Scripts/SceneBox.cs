using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SceneBox
{
    public Vector3Int ncells;
    //public float[] boxMatrix;
    public Texture3D TexMatrix;

    //sdf局部更新---MC局部更新
    public Bounds localBox;

    public ComputeShader SdfShader;

    public UseSdfTexShader texShader;

    const int offset = 2;

    public SceneBox(ComputeShader sdfShader)
    {
        localBox = new Bounds();
        SdfShader = sdfShader;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scriptableSDF"></param>
    /// <param name="save"></param>
    /// <param name="name">save texture3d and scriptable</param>
    public void SetScriptable(out ManagerScriptableObject scriptableSDF, bool save, string name)
    {
        scriptableSDF = ScriptableObject.CreateInstance<ManagerScriptableObject>();
        scriptableSDF.SetValue(ncells + Vector3Int.one, localBox, TexMatrix);

        if (save)
        {
            string texName = "Assets/SceneResult/Texture3D/" + name + ".asset";
            string srcName = "Assets/SceneResult/ScriptableObj/" + name + ".asset";
            if (TexMatrix != null)
            {
                AssetDatabase.CreateAsset(TexMatrix, texName);
            }
            if (scriptableSDF != null)
            {
                AssetDatabase.CreateAsset(scriptableSDF, srcName);
            }
        }
    }

    /// <summary>
    /// Update the first two models
    /// </summary>
    /// <param name="objA"></param>
    /// <param name="objB"></param>
    /// <param name="type"></param>
    /// <param name="moveToOrigin">Whether to move the new model back to the origin</param>
    public void UpdateSDF(MeshFilter objA, MeshFilter objB,BooleanType type,bool moveToOrigin, ref Vector3 origin)
    {
        Bounds boundsA = objA.GetComponent<Renderer>().bounds;
        Bounds boundsB = objB.GetComponent<Renderer>().bounds;
        Vector3 objAmin = boundsA.min;
        Vector3 objAmax = boundsA.max;
        Vector3 objBmin = boundsB.min;
        Vector3 objBmax = boundsB.max;

        //local box min max
        //包围盒总是非常紧凑的，但是对于SDF，我们不希望他这么紧凑，所以要加一个偏值量
        localBox.min = new Vector3(Mathf.Min(objAmin.x, objBmin.x) - offset * Constants.Step, Mathf.Min(objAmin.y, objBmin.y) - offset * Constants.Step, Mathf.Min(objAmin.z, objBmin.z) - offset * Constants.Step);
        localBox.max = new Vector3(Mathf.Max(objAmax.x, objBmax.x) + offset * Constants.Step, Mathf.Max(objAmax.y, objBmax.y) + offset * Constants.Step, Mathf.Max(objAmax.z, objBmax.z) + offset * Constants.Step);

        Vector3 boxSizef = localBox.size / Constants.Step;

        ncells = new Vector3Int((int)boxSizef.x, (int)boxSizef.y, (int)boxSizef.z);

        /////use computeshader
        texShader = new UseSdfTexShader(objA.transform, objB.transform, ncells, localBox.min, SdfShader);
        texShader.ComputeSDF(type, ref TexMatrix);

        if (moveToOrigin)
        {
            origin = localBox.center;
            localBox.center = Vector3.zero;
        }
    }

    /// <summary>
    /// Update objects with more than two models
    /// </summary>
    /// <param name="objB"></param>
    /// <param name="type"></param>
    /// <param name="moveToOrigin">Whether to move the new model back to the origin</param>
    public void UpdateSDFLater(MeshFilter objB, BooleanType type,bool moveToOrigin, ref Vector3 origin)
    {
        Bounds old = localBox;
        Vector3 objAmin = old.min;
        Vector3 objAmax = old.max;

        Bounds boundsB = objB.GetComponent<Renderer>().bounds;
        Vector3 objBmin = boundsB.min;
        Vector3 objBmax = boundsB.max;

        localBox.min = new Vector3(Mathf.Min(objAmin.x, objBmin.x) - offset * Constants.Step, Mathf.Min(objAmin.y, objBmin.y) - offset * Constants.Step, Mathf.Min(objAmin.z, objBmin.z) - offset * Constants.Step);
        localBox.max = new Vector3(Mathf.Max(objAmax.x, objBmax.x) + offset * Constants.Step, Mathf.Max(objAmax.y, objBmax.y) + offset * Constants.Step, Mathf.Max(objAmax.z, objBmax.z) + offset * Constants.Step);
        
        Vector3 boxSizef = localBox.size / Constants.Step;

        ncells = new Vector3Int((int)boxSizef.x, (int)boxSizef.y, (int)boxSizef.z);

        /////use computeshader
        texShader.SetSDFLater(TexMatrix, old, objB.transform, ncells, localBox.min);
        texShader.ComputeSDF(type, ref TexMatrix);

        if (moveToOrigin)
        {
            origin = localBox.center;
            localBox.center = Vector3.zero;
        }
    }

}
