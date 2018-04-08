using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ig_Targeting : MonoBehaviour {

    public ig_DashNode[] targetingNodes;

    public Transform GetNearestNode(Transform other) {
        Transform nearest = targetingNodes[0].transform;

        foreach(ig_DashNode node in targetingNodes) {
            if ((Vector3.Distance(other.position, node.transform.position) < Vector3.Distance(other.position, nearest.position)) && node.active) {
                nearest = node.transform;
            }
        }

        return nearest;
    }
}
