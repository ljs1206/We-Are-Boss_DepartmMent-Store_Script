using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FindMissingObject))]
public class FindMissingCustom : Editor
{
    private FindMissingObject _findMissingObject;
    void OnEnable()
    {
        _findMissingObject = (FindMissingObject)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _findMissingObject = (FindMissingObject)target;
        GUILayout.BeginHorizontal();
        {
            if(GUILayout.Button("Delete Missing Compo")){
                _findMissingObject.DeleteMissingCompo();
            }
        }
        GUILayout.EndHorizontal();
    }
}
