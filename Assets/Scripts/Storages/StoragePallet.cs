using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePallet : Storage {
    protected override bool CanStore(GameObject go) {
        return go.CompareTag("crate");
    }

    protected override void OnObjectAdded(GameObject go) {
        if (IsFull()) {
            //TODO create shelf in current position
        }
    }
}
