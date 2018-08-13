using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodStructure : MonoBehaviour {
    bool exploded = false;

    // Use this for initialization
    void Start () {
		
	}

    private bool CollidesWithHazard()
    {
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, new Vector3(0.4f, 0.8f, 0.4f), transform.rotation);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("hazard") && c.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (CollidesWithHazard())
            Explode();
    }

    private void Explode()
    {
        if (!exploded)
        {
            exploded = true;
            Destroy(gameObject);
        }
    }
}
