using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGage;

public class ig_KnifeAttack : ig_Ability {

    public GameObject parent;
    [HideInInspector]
    public ig_Knife knife;

    public bool canAttack;
    public bool attacking;
    public float frameFreezeTime;

    public void AttackInit(GameObject parent) {
        this.parent = parent;
        attacking = false;
        canAttack = true;
    }

    public void CheckAttack() {
        RaycastHit hit;

        if(Physics.Raycast(parent.transform.position, parent.transform.forward, out hit, knife.bladeLength)){
            ig_Health target = hit.collider.gameObject.GetComponent<ig_Health>();
            Hit knifeHit = new Hit(gameObject, target.gameObject, knife.damage);

            if (target != null)
                target.TakeHit(knifeHit);
        }
    }

    public override void StartAbility() {
        attacking = true;
        canAttack = false;
    }

    public void StopAttack() {
        attacking = false;
        canAttack = true;
    }

    void Update() {

    }

}
