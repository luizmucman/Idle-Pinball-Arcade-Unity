using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CameraSizeHandler))]
public class CSHEditor : Editor
{
    private CameraSizeHandler csh
    {
        get
        {
            return (CameraSizeHandler)target;
        }
    }

    private bool isAplied;

    void OnEnable()
    {
        csh.cam = csh.GetComponent<Camera>();
        csh.cam.orthographic = true;
        csh.thisTransform = csh.transform;
    }

    public override void OnInspectorGUI()
    {
        isAplied = csh.height == csh.cam.orthographicSize * 2;
        CheckAndAssignNullVariables();

        GUILayout.Space(10);
        if (!Application.isPlaying)
        {
            if (isAplied)
            {
                GUILayout.BeginHorizontal("Box");
                GUILayout.Label("Width: " + csh.width);
                GUILayout.Label("Height: " + csh.height);
                GUILayout.EndHorizontal();
            }
            else
            {
                if (GUILayout.Button("Set Projection Size"))
                    SetSizeAsDefault();
            }

            csh.keepBottomPos = GUILayout.Toggle(csh.keepBottomPos, "Keep Bottom Position");
        }
        if (GUI.changed)
            Undo.RecordObject(csh, "CameraSizeHandler");
        
        EditorUtility.SetDirty(target);
    }

    private void SetSizeAsDefault()
    {
        csh.height = csh.cam.orthographicSize * 2;
        csh.width = csh.height * GetMainGameViewSize().x / GetMainGameViewSize().y;
    }

    private void UpdateCameraSize()
    {
        csh.cam.orthographicSize = csh.width / GetMainGameViewSize().x * GetMainGameViewSize().y / 2;
        csh.UpdatePosition();
    }

    private Vector2 GetMainGameViewSize()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
        return (Vector2)Res;
    }

    private void CheckAndAssignNullVariables()
    {
        if (csh.cam == null)
            csh.cam = csh.GetComponent<Camera>();
        if (csh.thisTransform == null)
            csh.thisTransform = csh.transform;
    }
}