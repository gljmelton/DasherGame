using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using Rewired;
using IndieGage;

public class ig_CharacterController : MonoBehaviour {

    public float gravity = 9.8f;
    public LayerMask dashMask;
    public bool canPlay;

    private Player player;
    private CharacterController cc;

    //Movement
    private float vSpeed = 0f;
    private Vector3 velocity;

    //Input
    private float focusAxis = 0f;
    private float lsHorizontalAxis = 0f;
    private float lsVerticalAxis = 0f;
    private float rsHorizontalAxis = 0f;
    private float rsVerticalAxis = 0f;
    private Vector3 leftStickDirection;
    private Vector3 rightStickDirection;
    private float lastFocusAxis = 0f;

    //Ability
    private ig_Dash Dash;
    private ig_Focus Focus;
    private ig_Health Health;
    //private ig_Block block;
    private ig_KnifeAttack KnifeAttack;

    public ig_Knife currentKnife;
    //public ig_Shotgun currentShotgun;

    public Animator anim;

    //Targeting
    private GameObject currentTarget;

    // Use this for initialization
    void Start() {

        canPlay = true;
        
        //Setup Dash
        Dash = GetComponent<ig_Dash>();
        Dash.DashInit(gameObject);
        Dash.stopDash += StopDash;

        //Setup Focus
        Focus = GetComponent<ig_Focus>();
        Focus.FocusInit(gameObject);
        Focus.stopFocus += StopFocus;
        Focus.startFocus += StartFocus;

        //Setup Health
        Health = GetComponent<ig_Health>();
        Health.onDeath += Die;
        Health.onHit += TakeHit;

        //Setup Knife
        KnifeAttack = GetComponent<ig_KnifeAttack>();
        KnifeAttack.AttackInit(gameObject);
        if (currentKnife != null)
            KnifeAttack.knife = currentKnife;

        //Setup Animation
        anim = GetComponent<Animator>();
        anim.SetInteger("knifeType", currentKnife.knifeType);
        anim.SetFloat("attackSpeedMultiplier", currentKnife.speed);
        anim.SetBool("attacking", KnifeAttack.attacking);

        currentTarget = null;

        cc = GetComponent<CharacterController>();

        player = ReInput.players.GetPlayer(0);

        leftStickDirection = new Vector3(0f, 0f, 0f);
        rightStickDirection = new Vector3(0f, 0f, 0f);


    }

    // Update is called once per frame
    void Update() {
        //Get Inputs
        focusAxis = player.GetAxis("Focus");
        lsHorizontalAxis = player.GetAxis("LSHorizontal");
        lsVerticalAxis = player.GetAxis("LSVertical");
        rsHorizontalAxis = player.GetAxis("RSHorizontal");
        rsVerticalAxis = player.GetAxis("RSVertical");

        leftStickDirection.x = lsHorizontalAxis;
        leftStickDirection.z = lsVerticalAxis;
        rightStickDirection.x = rsHorizontalAxis;
        rightStickDirection.z = rsVerticalAxis;

        leftStickDirection.Normalize();
        rightStickDirection.Normalize();

        //Set required values
        velocity = Vector3.zero;
        if (cc.isGrounded) {
            vSpeed = 0f;
        }

        if (canPlay) {
            //Focus
            if (!Focus.focusing) {
                if (focusAxis >= 0.2f && lastFocusAxis < 0.2f) {
                    Focus.StartAbility();
                }

            }
            if (Focus.focusing) {
                if (focusAxis < 0.2f && !Dash.dashing && lastFocusAxis >= 0.2f) {
                    Focus.StopAbility();
                }
            }
            //Dash
            if ((leftStickDirection.x > 0.1f || leftStickDirection.x < -0.1f) || (leftStickDirection.z > 0.1f || leftStickDirection.z < -0.1f)) {
                Dash.CheckAbility(leftStickDirection, Focus.focusing);

                if (player.GetButtonDown("Dash")) {
                    Dash.Dash();
                }
            }
            Dash.UpdateDash();


            //Attacking
            if (KnifeAttack.canAttack) {
                if (player.GetButtonDown("Knife")) {
                    KnifeAttack.attacking = true;
                }
            }

        }

        //Apply gravity to velocity
        vSpeed -= gravity * Time.deltaTime;
        velocity.y = vSpeed;

        cc.Move(velocity * Time.deltaTime);

        //Move player
        cc.Move(velocity * Time.deltaTime);

        //Store current frame variables to last frame
        lastFocusAxis = focusAxis;

        UpdateAnimator();
    }

    //When player takes damage
    public void TakeHit(Hit hit) {
        if (!Health.invincible) {
            Health.TakeHit(hit);
        }
    }

    //When player dies.
    public void Die() {
        Debug.Log("Player has died!");
    }

    //When player switches knives. --Switching feature not implemented--
    public void SwitchKnife() {
        KnifeAttack.knife = currentKnife;
    }

    //Controls player animator.
    private void UpdateAnimator() {
        anim.SetBool("attacking", KnifeAttack.attacking);
    }

    //Collision
    void OnControllerColliderHit(ControllerColliderHit other) {

        //If the player dashes and hits something that isn't the ground. Used in the event a wall
        //or non-target enemy intercepts dash.
        if (other.collider.tag != "Ground") {
            if (Dash.dashing) {
                Dash.StopAbility();
            }
        }
    }

    //What happens when player starts focus.
    public void StartFocus() {
        //Prevent player from attacking while in focus.
        KnifeAttack.canAttack = false;
    }

    //What happens when player stops focus.
    public void StopFocus() {
        //Resets the dash ability once player stops using focus.
        Dash.ResetDash();
        KnifeAttack.canAttack = true;
    }

    //Delegate Trigger
    void StopDash() {
        Focus.StopAbility(); //Call OnStopAbility delegate to fire this in the player controller logic.
        gravity = 9.8f; //Same as above. Player controller will have a function to fire these.

        Quaternion initialDirection = transform.localRotation;

        if (Dash.dashTarget != null) {
            transform.LookAt(Dash.dashTarget.transform.position);
            transform.localRotation = Quaternion.Euler(new Vector3(initialDirection.eulerAngles.x, transform.localRotation.eulerAngles.y, initialDirection.eulerAngles.z));
        }

        else {
            
        }
    }
}
