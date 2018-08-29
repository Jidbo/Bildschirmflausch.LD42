using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class Recipe : ScriptableObject {
    [SerializeField]
    private List<GameObject> ingredients;
    [SerializeField]
    private GameObject result;
    [SerializeField]
    private int scoreForCrafting;

    public bool TryCraft(GameObject parent, List<GameObject> contents) {
        if (AreTagListsEqual(CreateTagList(contents), (CreateTagList(ingredients)))) {
            Craft(parent);
            return true;
        }
        return false;
    }

    private bool AreTagListsEqual (ArrayList list1, ArrayList list2) {
        if(list1.Count != list2.Count) {
            return false;
        }
        for (int i = 0; i < list1.Count; i++) {
            if (!list1[i].Equals(list2[i])) {
                return false;
            }
        }
        return true;
    }

    private void Craft(GameObject parent) {
        GameObject newGO = Instantiate(result);
        newGO.transform.position = parent.transform.position;
        newGO.transform.rotation = parent.transform.rotation;
        Destroy(parent);
        try {
            GameController.instance.updateScore(scoreForCrafting);
        } catch(Exception e) {
            Debug.Log(e);
        }
    }

    private ArrayList CreateTagList(List<GameObject> objects) {
        ArrayList tagList = new ArrayList();
        foreach (GameObject go in objects) {
            tagList.Add(go.tag);
        }
        tagList.Sort();
        return tagList;
    }
}
