using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePallet : Storage {
    [SerializeField]
    GameObject shelf;

    public StoragePallet() : base(new string[] { "crate" }) {

    }

    protected override void OnObjectAdded(GameObject go) {
        if (IsFull()) {
            GameObject newShelf = UnityEngine.Object.Instantiate(shelf);
            newShelf.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
