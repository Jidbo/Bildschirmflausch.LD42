using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePallet : Storage {
    [SerializeField]
    GameObject shelf;

    protected override bool CanStore(GameObject go) {
        return go.CompareTag("crate");
    }

    protected override void OnObjectAdded(GameObject go) {
        if (IsFull()) {
            GameObject newShelf = UnityEngine.Object.Instantiate(shelf);
            newShelf.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
