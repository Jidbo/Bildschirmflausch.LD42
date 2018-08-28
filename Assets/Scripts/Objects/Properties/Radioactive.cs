using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radioactive : MonoBehaviour {
    [SerializeField]
    float poisoningRadius = 1;
    [SerializeField]
    float poisoningStrength = 1f;
    [SerializeField]
    Vector3 sourceOffset;

    private void FixedUpdate() {
        Vector3 sourcePosition = GetSourcePosition();
        Collider[] colliders = Physics.OverlapSphere(sourcePosition, poisoningRadius);
        foreach (Collider c in colliders) {
            if (c.gameObject != gameObject) {
                Explosive explosive = c.gameObject.GetComponent<Explosive>();
                if (explosive != null) {
                    explosive.AddPoisoning(GetCorrectPoisoningAmount(c, sourcePosition));
                }
            }
        }
    }

    private Vector3 GetSourcePosition() {
        return transform.position
            + transform.right * sourceOffset.x
            + transform.up * sourceOffset.y
            + transform.forward * sourceOffset.z;
    }

    private float GetCorrectPoisoningAmount(Collider c, Vector3 sourcePosition) {
        float distance = (c.ClosestPoint(sourcePosition) - sourcePosition).magnitude;
        float distanceFactor = 1 - (distance / poisoningRadius);
        return poisoningStrength * Time.deltaTime * distanceFactor;
    }
}
