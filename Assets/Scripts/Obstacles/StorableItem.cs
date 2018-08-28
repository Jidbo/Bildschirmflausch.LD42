using UnityEngine;
using System.Collections;

public class StorableItem : MonoBehaviour {
    [SerializeField]
    private Vector3 size;

    public Vector3 GetSize() {
        return size;
    }
}
