using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePlayer : Storage {

    public StoragePlayer() : base (new string[] { "shelf", "crate", "pallet", "waste"})
    {

    }

    protected override void OnObjectAdded(GameObject go) {
        
    }
}
