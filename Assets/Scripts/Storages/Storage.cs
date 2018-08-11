using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storage : MonoBehaviour {
    [SerializeField]
    protected int maxCapacity;
    protected GameObject[] content;

    private void Start() {
        content = new GameObject[maxCapacity];
    }

    /// <summary>
    /// Adds the given GameObject to the first empty slot
    /// </summary>
    public bool AddToStorage(GameObject newGO) {
        if (CanStore(newGO)) {
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == null)
                {
                    content[i] = newGO;
                    OnObjectAdded(newGO);
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Gets called when an object is added to the storage (Used for crafting)
    /// </summary>
    protected abstract void OnObjectAdded(GameObject go);
    
    /// <summary>
    /// Checks whether or not the type of the given GameObject can be stored
    /// </summary>
    protected abstract bool CanStore(GameObject go);

    /// <summary>
    /// Removes and returns the first GameObject it finds
    /// </summary>
    public GameObject GetFromStorage() {
        for (int i = 0; i < content.Length; i++) {
            if (content[i] != null) {
                GameObject go = content[i];
                content[i] = null;
                return go;
            }
        }
        return null;
    }

    /// <summary>
    /// Returns true when no objects are stored
    /// </summary>
    public bool IsEmpty() {
        foreach (GameObject go in content) {
            if (go != null)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Returns true when the capacity limit is reached
    /// </summary>
    public bool IsFull()
    {
        foreach (GameObject go in content)
        {
            if (go == null)
                return false;
        }
        return true;
    }
}
