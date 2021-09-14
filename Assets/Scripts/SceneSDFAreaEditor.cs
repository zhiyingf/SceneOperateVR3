using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneSDFArea))]
public class SceneSDFAreaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SceneSDFArea scenesdfArea = target as SceneSDFArea;
        GUILayout.Space(8.0f);
        scenesdfArea.name = EditorGUILayout.TextField("Name", scenesdfArea.name);
        GUILayout.Space(8.0f);
        bool executeflag = EditorGUILayout.Toggle("Execute in Editor", scenesdfArea.isEditor, new GUILayoutOption[0]);
        if(executeflag != scenesdfArea.isEditor)
        {
            scenesdfArea.isEditor = executeflag;
            scenesdfArea.living = false;
            scenesdfArea.StopAllCoroutines();
        }

        //var Operations = scenesdfArea.Operations;
        //int count = Mathf.Max(0, EditorGUILayout.IntField("size", Operations.Count));
        //while (count < Operations.Count)
        //{
        //    Operations.RemoveAt(Operations.Count - 1);
        //}
        //while (count > Operations.Count)
        //{
        //    Operations.Add(null);
        //}
        //for(int i = 0; i < Operations.Count; i++)
        //{
        //    Operations[i] = (MeshFilter)EditorGUILayout.ObjectField(Operations[i], typeof(MeshFilter));
        //}

        GUILayout.Space(8.0f);
        scenesdfArea.McShader = (ComputeShader)EditorGUILayout.ObjectField("McShader", scenesdfArea.McShader, typeof(ComputeShader), true);
        scenesdfArea.SdfShader = (ComputeShader)EditorGUILayout.ObjectField("SdfShader", scenesdfArea.SdfShader, typeof(ComputeShader), true);
        GUILayout.Space(8.0f);
        scenesdfArea.setColliderLeft = (SetCollider)EditorGUILayout.ObjectField("SetColliderLeft", scenesdfArea.setColliderLeft, typeof(SetCollider), true);
        scenesdfArea.setColliderRight = (SetCollider)EditorGUILayout.ObjectField("SetColliderRight", scenesdfArea.setColliderRight, typeof(SetCollider), true);
        GUILayout.Space(8.0f);

        scenesdfArea.offset = EditorGUILayout.Vector3Field("Offset", scenesdfArea.offset);

        GUILayout.Space(8.0f);

        if (GUILayout.Button("Update Mesh"))
        {
            scenesdfArea.ExecuteOnClick();
        }

        if (GUILayout.Button("Save Mesh", new GUILayoutOption[0]))//Save Prefab
        {
            //string text = EditorUtility.SaveFilePanelInProject("Save Result", scenesdf.gameObject.name, "prefab", "Please select where do you want to save the result?");
            string text = EditorUtility.SaveFilePanelInProject("Save Result", scenesdfArea.gameObject.name, "obj", "Please select where do you want to save the result?");

            if (text != null)
            {
                //��������.obj
                GameObject gameObject = (GameObject)Instantiate(scenesdfArea.gameObject);
                ExportMesh.ExportMeshToObj(gameObject, text);
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
