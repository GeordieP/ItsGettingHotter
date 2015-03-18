using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    //public Transform target;
    public Vector3 target = Vector3.zero;
    Vector3 positionDelta = Vector3.zero;
    private float distance = 15.0f;
    private float rotateSpeed = 2.0f;

    private float minDist = 0.0f;
    private float maxDist = 50000.0f;

    private Vector2 rotationVector = new Vector2(315.0f, 35.0f);

	void Update () {
        if (Input.GetMouseButton(1)) {
            rotationVector.x += Input.GetAxis("Mouse X") * rotateSpeed;
        }

        positionDelta.x += Input.GetAxis("Horizontal");
        positionDelta.z += Input.GetAxis("Vertical");

        Quaternion rotation = Quaternion.Euler(rotationVector.y, rotationVector.x, 0);
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * distance, minDist, maxDist);
        Vector3 distanceVector = new Vector3(0.0f, 0.0f, -distance);
        transform.rotation = rotation;

        Vector3 newPosD = Quaternion.Euler(0, rotationVector.x, 0) * positionDelta;

        Vector3 position = (rotation * distanceVector + target) + newPosD;
        transform.position = position;
	}
}
