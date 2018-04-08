using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ig_TimeTracker : MonoBehaviour {

    public GameObject target;
    public Transform node1, node2;

	// Use this for initialization
	void Start () {
        target.transform.position = node2.transform.position;
        MoveToNode1();
	}
	
	void MoveToNode1() {
        iTween.MoveTo(target, iTween.Hash("position", node1.position, "time", 1f, "oncompletetarget", gameObject, "oncomplete", "MoveToNode2"));
    }

    void MoveToNode2() {
        iTween.MoveTo(target, iTween.Hash("position", node2.position, "time", 1f, "oncompletetarget", gameObject, "oncomplete", "MoveToNode1"));
    }
}
