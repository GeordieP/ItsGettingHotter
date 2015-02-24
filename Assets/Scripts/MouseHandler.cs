using UnityEngine;
using System.Collections;

public class MouseHandler : MonoBehaviour {

    //public GameObject theUnit;

    public Unit[] units;

	// Use this for initialization
	void Start () {
        units = GameObject.FindObjectsOfType<Unit>();
        foreach (Unit unit in units) {
            unit.SetColor();
        }
	}
	
	// Update is called once per frame
	void Update () {
        Ray theray;
        RaycastHit hitinfo;

        if (Input.GetMouseButtonDown(0)) {
            theray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(theray, out hitinfo)) {
                if (hitinfo.transform.gameObject.tag == "Node") {
                    foreach (Unit unit in units) {
                        unit.AddTarget(hitinfo.transform.gameObject.transform);
                    }
                    hitinfo.transform.GetComponent<Node>().ToggleSelected();
                }
            }
        }
	}
}
