using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {

    public Transform planet;
    private float distance = 20.0f;
    private float rotateSpeed = 2.0f;

    private float minDist = 15.0f;
    private float maxDist = 30.0f;

    private Vector2 rotationVector = Vector2.zero;       // TODO: Rename

    void Update() {
        if (planet) {

            if (Input.GetMouseButton(1)) {
                rotationVector.x += Input.GetAxis("Mouse X") * rotateSpeed;
                rotationVector.y -= Input.GetAxis("Mouse Y") * rotateSpeed;
            }

            Quaternion rotation = Quaternion.Euler(rotationVector.y, rotationVector.x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, minDist, maxDist);
            Vector3 distanceVector = new Vector3(0.0f, 0.0f, -distance);

            Vector3 position = rotation * distanceVector + planet.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
