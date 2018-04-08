using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Knife", menuName = "Weapons/Knife", order = 1)]
public class ig_Knife : ScriptableObject {

    public string knifeName;
    public string knifeDescription;
    public Sprite knifeIcon;
    public GameObject knifeObject;
    public Transform bladeEdge;
    public float bladeLength;
    public float baseCooldown; 
    public float speed; //Speed multiplier used by animator
    public float knockback;
    public float stun;
    public int damage;
    public int knifeType;
    public AudioClip[] sounds;

}
