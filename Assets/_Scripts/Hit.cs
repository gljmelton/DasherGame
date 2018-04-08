using UnityEngine;

namespace IndieGage {
    public class Hit {

        public GameObject sender;
        public GameObject receiver;
        public int hitPoints;
        public float stunValue;
        public float knockBackValue;

        public Hit(GameObject sender, GameObject receiver, int hitPoints) {
            this.sender = sender;
            this.receiver = receiver;
            this.hitPoints = hitPoints;
            stunValue = 0f;
            knockBackValue = 0f;
        }

        public Hit(GameObject sender, GameObject receiver, int hitPoints, float stunValue, float knockBackValue) {
            this.sender = sender;
            this.receiver = receiver;
            this.hitPoints = hitPoints;
            this.stunValue = stunValue;
            this.knockBackValue = knockBackValue;
        }

    }
}
