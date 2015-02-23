using UnityEngine;
using System.Collections;

public class MouseHandler : MonoBehaviour {

    public GameObject theUnit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Ray theray;
        RaycastHit hitinfo;

        if (Input.GetMouseButtonDown(0)) {
            theray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(theray, out hitinfo)) {
                //playerCharacterScript.AddTarget(hitinfo.transform.gameObject);
                //theUnit.GetComponent<Unit>().SetTarget(hitinfo.point);
                //print("the position: " + hitinfo.transform.position.ToString());
                if (hitinfo.transform.gameObject.tag == "Node") {
                    //theUnit.GetComponent<Unit>().SetTarget(hitinfo.transform.gameObject.transform);       // pass the node to the unit
                    theUnit.GetComponent<Unit>().AddTarget(hitinfo.transform.gameObject.transform);
                    hitinfo.transform.GetComponent<Node>().ToggleSelected();
                }
            }
        } else if (Input.GetMouseButton(1)) {
            // do it differently for nodes
        }
	}
}
