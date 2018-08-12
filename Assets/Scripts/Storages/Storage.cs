using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storage : MonoBehaviour {
    
    protected int maxCapacity;

    [SerializeField]
    protected GameObject[] content;
    [SerializeField]
    protected List<Transform> positions;
    [SerializeField]
    protected int currentAmount;
    protected ArrayList storableTags = new ArrayList();

    private void Start() {
        maxCapacity = positions.Count;
        currentAmount = 0;
        content = new GameObject[maxCapacity];
    }

    /// <summary>
    /// Constructor for a storage object.
    /// </summary>
    /// <param name="storableTags">A list of tags which can be stored.</param>
    /// <param name="maxCapacity">The capacity of this storage.</param>
    public Storage(string[] storableTags) {
        this.storableTags.AddRange(storableTags);
    }

    /// <summary>
    /// Adds the given GameObject to the first empty slot
    /// </summary>
    /// <param name="newGO">The gameobject which should be stored.</param>
    /// <returns>Returns true, if the object is storable</returns>
    public bool AddToStorage(GameObject newGO) {
        if (!IsFull() && CanStore(newGO)) {
            currentAmount++;
            content[currentAmount - 1] = newGO;
            OnObjectAdded(newGO);
            newGO.transform.parent = GetCorrectParent();
            newGO.transform.localPosition = Vector3.zero;
            newGO.transform.rotation = new Quaternion();
            Rigidbody rb = newGO.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            return true;
		} else {
            return false;
        }
    }

    private Transform GetCorrectParent() {
        return positions[currentAmount - 1];
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
    protected virtual bool CanStore(GameObject go) {
        return storableTags.Contains(go.tag);
    }

    /// <summary>
    /// Removes and returns the first GameObject it finds
    /// </summary>
    /// <returns>The first gameobject found.</returns>
    public GameObject GetFromStorage() {
        if (!IsEmpty()) {
            currentAmount--;
            GameObject lastItem = content[currentAmount];
            content[currentAmount] = null;
            lastItem.transform.parent = null;
            Rigidbody rb = lastItem.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            return lastItem;
        }
        return null;
    }

    /// <summary>
    /// Returns true when no objects are stored
    /// </summary>
    public bool IsEmpty() {
        return currentAmount == 0;
    }

    /// <summary>
    /// Returns true when the capacity limit is reached
    /// </summary>
    public bool IsFull() {
        return currentAmount == maxCapacity;
    }

    public bool Contains(GameObject go) {
        foreach(GameObject g in content) {
            if(g == go) {
                return true;
            }
        }
        return false;
    }
}
