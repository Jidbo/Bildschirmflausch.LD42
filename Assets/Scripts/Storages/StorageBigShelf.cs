using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBigShelf : Storage
{

    protected override bool CanStore(GameObject go)
    {
        if (go.CompareTag("shelf") || go.CompareTag("waste"))
        {
            return true;
        }
        else return false;
    }

    protected override void OnObjectAdded(GameObject go)
    {
        // nothing to craft
    }
}
