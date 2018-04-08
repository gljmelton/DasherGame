using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGage;

public class ig_BasicEnemy : MonoBehaviour {

    public ig_Health Health;
    public ig_Targeting Targeting;
    public float dashRadius;

    // Use this for initialization
    void Start() {
        Targeting = GetComponent<ig_Targeting>();

        foreach(ig_DashNode node in Targeting.targetingNodes) {
            node.transform.localPosition *= dashRadius;
        }

        Health = GetComponent<ig_Health>();
        Health.onDeath += Die;
        Health.onHit += TakeHit;
    }

    // Update is called once per frame
    void Update() {

    }

    void TakeHit(Hit hit) {
        Debug.Log(gameObject.name + "\"I'm Hit!\"");
    }

    void Die() {
        Destroy(gameObject);
    }
}
