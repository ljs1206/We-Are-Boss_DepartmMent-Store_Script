using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FindMissingObject : MonoBehaviour
{
#if UNITY_EDITOR
    string getObjectHierarchy(GameObject go)
    {
        string path = go.name;
        Transform tr = go.transform;

        while (tr.parent != null)
        {
            path = tr.parent.name + " / " + path;
            tr = tr.parent;
        }

        return path;
    }

    public void DeleteMissingCompo()
    {
        GameObject[] all = FindObjectsOfType<GameObject>();

        foreach (GameObject go in all)
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        }
    }
#endif
}