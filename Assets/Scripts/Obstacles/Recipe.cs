using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class Recipe : ScriptableObject {
    [SerializeField]
    private List<GameObject> ingredients;
    [SerializeField]
    private GameObject result;

    public bool TryCraft(GameObject parent, List<GameObject> contents) {
        //Solution is not very good...
        if (CreateTagString(contents).CompareTo(CreateTagString(ingredients)) == 0) {
            Craft(parent);
            return true;
        }
        return false;
    }

    private void Craft(GameObject parent) {
        GameObject newGO = Instantiate(result);
        newGO.transform.position = parent.transform.position;
        newGO.transform.rotation = parent.transform.rotation;
        Destroy(parent);
    }

    private string CreateTagString(List<GameObject> objects) {
        List<string> tagList = new List<string>();
        string tagString = "";
        foreach (GameObject go in objects) {
            tagList.Add(go.tag);
        }
        tagList.Sort();
        foreach(string s in tagList) {
            tagString += s;
        }
        return tagString;
    }
}
