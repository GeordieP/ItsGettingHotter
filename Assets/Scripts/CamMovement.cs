using UnityEngine;
using System.Collections;

public class CamMovement : MonoBehaviour {
	public float height;
	public float distance;
	public float rotation;
	public float rotSpeed;
	public float moveSpeed;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (distance, height, 0);
		transform.LookAt (Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () {
		//Rotate camera around itself using the RIGHT mouse button
		if (Input.GetMouseButton (1)) {
			rotation = Input.GetAxis("Mouse X") * rotSpeed;
			transform.RotateAround(transform.position, Vector3.up, rotation);
		}

		if(Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			transform.position += transform.forward * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel") * moveSpeed * 100.0f;
		}
		//These all mose the camera based on which key is pressed
		if(Input.GetKey(KeyCode.W))
		{
			transform.position += Vector3.Scale (transform.forward, new Vector3(1,0,1)) * Time.deltaTime * moveSpeed;
		}
		if(Input.GetKey(KeyCode.A))
		{
			transform.position += Vector3.Scale (-transform.right, new Vector3(1,0,1)) * Time.deltaTime * moveSpeed;
		}
		if(Input.GetKey(KeyCode.S))
		{
			transform.position += Vector3.Scale (-transform.forward, new Vector3(1,0,1)) * Time.deltaTime * moveSpeed;
		}
		if(Input.GetKey(KeyCode.D))
		{
			transform.position += Vector3.Scale (transform.right, new Vector3(1,0,1)) * Time.deltaTime * moveSpeed;
		}
	}
}
