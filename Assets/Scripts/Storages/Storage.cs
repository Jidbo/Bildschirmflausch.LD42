﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storage : MonoBehaviour
{
    [SerializeField]
    protected int maxCapacity;
    protected GameObject[] content;

    ArrayList storableTags;

    private void Start()
    {
        content = new GameObject[maxCapacity];
    }

    /// <summary>
    /// Constructor for a storage object.
    /// </summary>
    /// <param name="storableTags">A list of tags which can be stored.</param>
    public Storage(string[] storableTags)
    {
        foreach (string i in storableTags)
            this.storableTags.Add(i);
    }

    /// <summary>
    /// Adds the given GameObject to the first empty slot
    /// </summary>
    /// <param name="newGO">The gameobject which should be stored.</param>
    /// <returns>Returns true, if the object is storable</returns>
    public bool AddToStorage(GameObject newGO)
    {
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
    /// <param name="go">The object which should be added.</param>
    protected abstract void OnObjectAdded(GameObject go);
    
    /// <summary>
    /// Checks whether or not the type of the given GameObject can be stored
    /// </summary>
    /// <param name="go">The Gameobject wich should be checked.</param>
    /// <returns>True, if the object is storable.</returns>
    protected bool CanStore(GameObject go)
    {
        return storableTags.Contains(go.tag);
    }

    /// <summary>
    /// Removes and returns the first GameObject it finds
    /// </summary>
    /// <returns>The first gameobject found.</returns>
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