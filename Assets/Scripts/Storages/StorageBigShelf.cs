using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBigShelf : Storage {
    public StorageBigShelf() : base(new string[] { "shelf", "waste", "pallet", "chest"}, 4) {

    }

    protected override void OnObjectAdded(GameObject go) {
        // nothing to craft
    }
}
 