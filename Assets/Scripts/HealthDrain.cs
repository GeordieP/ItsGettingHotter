using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthDrain : MonoBehaviour {

	public float speed;
	Image img;
	// Use this for initialization
	void Start () {
		 img =  GameObject.Find("Health").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= new Vector3(speed * Time.deltaTime, 0,0);
		if (transform.localPosition.x >= 0.0f)
			transform.localPosition = new Vector3 (0, 0, 0);
		else if (transform.localPosition.x <= -210.0f)
			img.color = UnityEngine.Color.yellow;
		if (transform.localPosition.x <= -315.0f)
			img.color = UnityEngine.Color.red;
		if (transform.localPosition.x >= -210.0f)
			img.color = UnityEngine.Color.green;

		speed *= 1.0008f;
		if (speed >= 40.0f)
			speed = 40.0f;
		if(transform.localPosition.x <= -410.0f)
		{
			GameObject menu = GameObject.Find ("Main Camera");
			MenuOpen MO = menu.GetComponent<MenuOpen> ();
			MO.EndGame ();
		}
		//Debug.Log (transform.localPosition.x);


	}
}
