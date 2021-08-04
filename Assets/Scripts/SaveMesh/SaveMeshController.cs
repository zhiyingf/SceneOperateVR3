using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveMeshController : MonoBehaviour
{
    [SerializeField]
    private SceneSDFArea scenesdfArea = null;

    [SerializeField]
    private string ExportFolderName = "SceneResult";

    public void ExportToOBJ(string fileName = null)
    {
        string name = fileName ?? DefaultFileName();
        string path = Path.Combine(Application.dataPath, ExportFolderName);//Unity Editor: <path to project folder>/Assets
        TryCreateDirectory(path);

        name = Path.Combine(path, name + ".obj");
        GameObject gameObject = Instantiate(scenesdfArea.gameObject);
        ExportMesh.ExportMeshToObj(gameObject, name);
    }

    private string DefaultFileName()
    {
        return (DateTime.Now).ToString("yyyyMMddHHmmss");
    }

    private void TryCreateDirectory(string path)
    {
        // Try to create the directory
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

        }
        catch (IOException ex)
        {
            //Console.WriteLine(ex.Message);
            Debug.Log(ex.Message);
        }
    }
}
