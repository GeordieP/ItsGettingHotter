/*/
* The most common aspect ratio for COMPUTERS is 16:9 followed by 16:10
* Possible aspect ratios for MOBILE are:
* 4:3
* 3:2
* 16:10
* 5:3
* 16:9
/*/
using UnityEngine;
using System.Collections;

public class GUIScaleTool : MonoBehaviour {

	
	void Start () 
	{
		this.transform.localScale = new Vector3 (Screen.width/1920.0f, Screen.height/1080.0f, 1.0f);
	}

	void Update ()
	{
		this.transform.localScale = new Vector3 (Screen.width/1920.0f, Screen.height/1080.0f, 1.0f);
	}
}
