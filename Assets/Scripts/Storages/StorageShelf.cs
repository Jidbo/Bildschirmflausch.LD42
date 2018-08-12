using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageShelf : Storage {
    [SerializeField]
    GameObject bigShelf;

    public StorageShelf() : base(new string[] { "waste", "shelf" }) {

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
                foreach (GameObject c in content) {
                    Destroy(c);
                }
                Destroy(gameObject);
            }
        }
    }
}
