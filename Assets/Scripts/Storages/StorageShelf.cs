using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageShelf : Storage {
    [SerializeField]
    GameObject bigShelf;

    public StorageShelf() : base(new string[] { "waste", "shelf" }) {

    }

    protected override void OnObjectAdded(GameObject go) {
        if (go.CompareTag("shelf")) {
            for (int i = 1; i < currentAmount - 1; i++) {
                if (content[i] != null) {
                    return;
                }
            }
            GameObject newBigShelf = UnityEngine.Object.Instantiate(bigShelf);
            newBigShelf.transform.position = transform.position;
            foreach(GameObject c in content) {
                Destroy(c);
            }
            Destroy(gameObject);
        }
    }
}
