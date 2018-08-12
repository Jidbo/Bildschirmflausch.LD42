using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBigShelf : Storage {
    public StorageBigShelf() : base(new string[] { "crate", "waste" }) {

    }

    protected override void OnObjectAdded(GameObject go) {
        // nothing to craft
    }

    protected override void OnObjectRemoved(GameObject go) {
        
    }
}
 