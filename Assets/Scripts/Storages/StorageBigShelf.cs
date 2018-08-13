using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBigShelf : Storage {
    bool wasFull;
    public StorageBigShelf() : base(new string[] { "crate", "waste" }) {
        GameController.instance.updateScore(10);
        wasFull = false;
    }

    protected override void OnObjectAdded(GameObject go) {
        // nothing to craft
        if (IsFull() || !wasFull) {
            wasFull = true;
            GameController.instance.updateScore(70);
        }
    }

    protected override void OnObjectRemoved(GameObject go) {
        
    }
}
 