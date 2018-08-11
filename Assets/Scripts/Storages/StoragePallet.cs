using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePallet : Storage {
    [SerializeField]
    GameObject shelf;

    public StoragePallet() : base(new string[] { "crate" }, 2) {

    }

    protected override void OnObjectAdded(GameObject go) {
        if (IsFull()) {
            GameObject newShelf = UnityEngine.Object.Instantiate(shelf);
            newShelf.transform.position = transform.position;
            foreach (GameObject c in content) {
                Destroy(c);
            }
            Destroy(gameObject);
        }
    }
}
