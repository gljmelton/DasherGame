using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ig_Rotator : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        iTween.RotateUpdate(gameObject, gameObject.transform.localRotation.eulerAngles + new Vector3(0, 1, 0), .1f);
	}
}
