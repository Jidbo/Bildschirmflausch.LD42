using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageShelf : Storage
{
    public StorageShelf() : base(new string[] { "crate", "pallet", "waste" })
    {

    }

    protected override void OnObjectAdded(GameObject go)
    {
        // nothing to craft
    }
}
