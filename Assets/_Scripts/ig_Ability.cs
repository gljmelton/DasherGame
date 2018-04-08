using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGage {
    public class ig_Ability : MonoBehaviour {


        public virtual void CheckAbility() {
            Debug.Log("Default ability check called");
        }

        public virtual void CheckAbility<T>(T param) {
            Debug.Log("Default ability check with arg called");
        }

        // Use this for initialization
        public virtual void StartAbility() {
            Debug.Log("Default ability start called");
        }

        // Update is called once per frame
        public virtual void UpdateAbility() {
            Debug.Log("Default ability update called");
        }

        public virtual void StopAbility() {
            Debug.Log("Default ability stop called");
        }
    }
}