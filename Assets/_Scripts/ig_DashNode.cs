using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ig_DashNode : MonoBehaviour {

	private int sides;
    public bool active;
    public LayerMask checkMask;

    void Start() {
        active = true;
        
    }

    void Update() {
        if (Physics.CheckSphere(transform.position, 1f, checkMask)) {
            active = false;
        } else active = true;
    }

    

}
