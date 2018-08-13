using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageShelf : Storage {
    [SerializeField]
    GameObject bigShelf;

    public StorageShelf() : base(new string[] { "waste", "shelf" , "crate" }) {

    }

    protected override bool CanStore(GameObject go) {
        if (go.CompareTag("shelf")) {
            return IsEmpty();
        }
        return storableTags.Contains(go.tag);
    }

    protected override void OnObjectAdded(GameObject go) {
        if (go.CompareTag("shelf")) {
            if (currentAmount == 1) {
                GameObject newBigShelf = UnityEngine.Object.Instantiate(bigShelf);
                newBigShelf.transform.position = transform.position;
                newBigShelf.transform.rotation = transform.rotation;
                foreach (GameObject c in content) {
                    Destroy(c);
                }
                GameController.instance.updateScore(50);
                Destroy(gameObject);
            }
        }
    }

    protected override void OnObjectRemoved(GameObject go) {

    }
}
