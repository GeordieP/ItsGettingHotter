using UnityEngine;
using System.Collections;

public class MenuRotation : MonoBehaviour {
	public float rotSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, Vector3.up, rotSpeed * Time.deltaTime);
	}
}
