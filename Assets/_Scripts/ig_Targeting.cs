using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ig_Targeting : MonoBehaviour {

    public ig_DashNode[] targetingNodes;

    /*
     * This is a pretty inefficient way of doing this. what would be best is to:
     * 1) Take in a Vector3, no reason to get a Transform, just makes calling this function convoluted
     * 2) Compare the angle of the supplied vector to the angle of each node and see which is nearest.
     * 
     * Although the code would be the same length it wouldn't need to use Vector3.Distance()
     * 
     */
    public Transform GetNearestNode(Transform other) {
        //Start with the node at 0
        Transform nearest = targetingNodes[0].transform;

        //Loop through each node and check to see what node the marker is closest to
        foreach(ig_DashNode node in targetingNodes) {
            if ((Vector3.Distance(other.position, node.transform.position) < Vector3.Distance(other.position, nearest.position)) && node.active) {
                nearest = node.transform;
            }
        }

        return nearest;
    }
}
