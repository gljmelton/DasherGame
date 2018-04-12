using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGage {
    public class ig_Dash : ig_Ability{

        delegate void OnStartDash();
        public delegate void OnStopDash();
        public OnStopDash stopDash;

        private GameObject parent;
        private CharacterController cc;

        [Header("Dash Stats")]
        public float dashSpeed = 12f;
        public float dashDistance = 15f;
        public float dashTargetRadius = 1.5f;
        public float dashSearchArea = 2f;
        public bool canDash = true;
        public bool dashing = false;
        public LayerMask dashMask;

        [Header("Dash Positionals")]
        private Vector3 dashPoint;
        private Vector3 potentialDashPoint;
        //private Vector3 lastDashPoint;
        private int dashSector = 0;
        public GameObject dashTarget = null;
        private GameObject potentialTarget;
        public GameObject dashTargetMarker;
        public GameObject dashAroundMarker;

        //Initializier for dash
        public void DashInit(GameObject parent) {
            this.parent = parent;
            cc = parent.GetComponent<CharacterController>();

            dashTargetMarker.SetActive(false);
            dashAroundMarker.SetActive(false);
            canDash = true;
        }

        public void CheckAbility(Vector3 leftStick, bool focusing) {
            
            if (focusing) {
                //Enable the targeting objects if using focus
                dashAroundMarker.SetActive(true);
                dashTargetMarker.SetActive(true);

                //Spherecast out from player to find a potential target
                RaycastHit hit;
                if (Physics.SphereCast(parent.transform.position, cc.radius * dashSearchArea, leftStick, out hit, dashDistance, dashMask)) {
                    if (hit.transform.tag == "Target") {
                        //Store the potential target of your dash
                        potentialTarget = hit.transform.gameObject;

                        //Get the nearest dash point around the target
                        ig_Targeting target = potentialTarget.GetComponent<ig_Targeting>();
                        potentialDashPoint = target.GetNearestNode(transform).position;
                    }

                } else {
                    //if the raycast hits nothing make the target nothing
                    potentialTarget = null;
                    
                    //store the point to dash to at the dash distance
                    potentialDashPoint = new Vector3(parent.transform.position.x + (leftStick.x * dashDistance), parent.transform.position.y, parent.transform.position.z + (leftStick.z * dashDistance));
                    
                }

                //Place the dash target markers or remove them
                if (potentialTarget != null) {
                    dashTargetMarker.transform.position = potentialTarget.transform.position;
                    dashAroundMarker.transform.position = potentialDashPoint;
                } else {
                    dashTargetMarker.transform.position = potentialDashPoint;
                    dashAroundMarker.transform.position = potentialDashPoint;
                }

                //lastDashPoint = potentialDashPoint;

            } 

            //If not focusing
            else {
                if (dashTarget != null) {
                    if ((leftStick.x > 0.1f || leftStick.x < -0.1f) || (leftStick.z > 0.1f || leftStick.z < -0.1f)) {

                        //Get the point the left stick is being held
                        potentialDashPoint = dashTarget.transform.position;
                        potentialDashPoint = new Vector3(potentialDashPoint.x + (leftStick.x * dashTargetRadius), potentialDashPoint.y, potentialDashPoint.z + (leftStick.z * dashTargetRadius));
                        dashAroundMarker.transform.position = potentialDashPoint;

                        //Use that point to find the nearest node that can be dashed to
                        ig_Targeting targeting = dashTarget.GetComponent<ig_Targeting>();
                        potentialDashPoint = targeting.GetNearestNode(dashAroundMarker.transform).position;

                        //Enable and set the dash marker
                        if(dashAroundMarker.activeSelf==false)
                            dashAroundMarker.SetActive(true);
                        dashAroundMarker.transform.position = potentialDashPoint;

                    }else {
                        if (dashAroundMarker.activeSelf == true)
                            dashAroundMarker.SetActive(false);
                    }
                } else {
                    
                    potentialDashPoint = parent.transform.position;
                    dashAroundMarker.SetActive(false);
                }
            }

        }

        public void Dash() {
            if (canDash) {
                //Apply potential points to actual points
                dashPoint = potentialDashPoint;
                dashTarget = potentialTarget;

                Debug.Log("Dash Point: " + dashPoint);
                Debug.Log("Dash Target: " + dashTarget);

                //Place Players on ground
                RaycastHit hit;
                Physics.Raycast(dashPoint, Vector3.down, out hit);
                dashPoint = hit.point;
                dashPoint.y += (cc.height / 2f) + cc.skinWidth;

                //teleport player
                parent.transform.position = dashPoint;

                dashAroundMarker.SetActive(false);
                dashTargetMarker.SetActive(false);
                stopDash();
            }
        }

        // This gets checked every frame the player is in control
        public void UpdateDash() {
            if (dashTarget != null) {
                if (Vector3.Distance(parent.transform.position, dashTarget.transform.position) > 5f) {
                    Debug.Log("Clearing parent");
                    dashTarget = null;
                    potentialTarget = null;
                    dashAroundMarker.SetActive(false);
                    dashTargetMarker.SetActive(false);
                }
            }

        }

        public void ResetDash() {
            potentialTarget = dashTarget;
            potentialDashPoint = dashPoint;
            dashAroundMarker.SetActive(false);
            dashTargetMarker.SetActive(false);
        }

        public override void StopAbility() {
            dashing = false;
            canDash = true;
            dashAroundMarker.SetActive(false);
            dashTargetMarker.SetActive(false);
            stopDash();
            //StopFocus(); //Call OnStopAbility delegate to fire this in the player controller logic.
            //gravity = 9.8f; //Same as above. Player controller will have a function to fire these.
        }
    }
}
