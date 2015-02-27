using UnityEngine;
using System.Collections;

public class DisablerBox : MonoBehaviour {
    void OnTriggerEnter(Collider otherCollider) {
        // when a unit is inside this disabler box, disallow selection of it by the player
        if (otherCollider.gameObject.tag == "Unit") {
            otherCollider.gameObject.GetComponent<Unit>().selectable = false;
        }
    }

    void OnTriggerExit(Collider otherCollider) {
        // reallow selection once the unit has left the disabler box
        if (otherCollider.gameObject.tag == "Unit") {
            otherCollider.gameObject.GetComponent<Unit>().selectable = true;
        }
    }
}
