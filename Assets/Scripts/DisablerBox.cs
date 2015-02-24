using UnityEngine;
using System.Collections;

public class DisablerBox : MonoBehaviour {
    void OnTriggerEnter(Collider otherCollider) {
        otherCollider.gameObject.SetActive(false);
    }

    void OnCollisionExit(Collision collisionInfo) {
        collisionInfo.gameObject.SetActive(false);
    }
}
