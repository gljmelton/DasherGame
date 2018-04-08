using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGage;

public class ig_Health : ig_Ability {

    public delegate void OnDeath();
    public delegate void OnHit(Hit hit);
    public OnHit onHit;
    public OnDeath onDeath;

    public float currentHealth = 100f;
    public float totalHealth = 100f;
    public float regenAmount = 0f;

    public bool invincible;

    public void HealthInit() {
        invincible = false;
    }

    public void ChangeHealth(float change) {

        

        currentHealth += change;

        if (currentHealth <= 0) {
            onDeath();
        }
    }

    public void InstaKill() {
        ChangeHealth(-currentHealth);
    }

    public void TakeHit(Hit hit) {
        if (!invincible) {
            ChangeHealth(-hit.hitPoints);
            onHit(hit);
        }
    }

    public override void UpdateAbility() {
        currentHealth += regenAmount * Time.deltaTime;
    }

}