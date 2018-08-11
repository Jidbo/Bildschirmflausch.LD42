﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePlayer : Storage {
    protected override bool CanStore(GameObject go) {
        return go.CompareTag("shelf") || go.CompareTag("crate") || go.CompareTag("pallet") || go.CompareTag("waste");
    }

    protected override void OnObjectAdded(GameObject go) {
        
    }
}
