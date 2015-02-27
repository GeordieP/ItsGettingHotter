using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseHandler : MonoBehaviour {

    //public GameObject theUnit;

    public Unit[] allUnits;
    public List<Unit> selectedUnits;
    private Vector3 initialMousePos = Vector3.zero;
    private Vector3 finalMousePos = Vector3.zero;
    public Canvas selectionboxCanvas;
    private GameObject selectionboxImage;

	// Use this for initialization
    void Start() {
        allUnits = GameObject.FindObjectsOfType<Unit>();
        selectedUnits = new List<Unit>();

        selectionboxCanvas.enabled = false;
        selectionboxImage = selectionboxCanvas.transform.FindChild("Panel").gameObject;
    }

	// Update is called once per frame
	void Update () {
        Ray theray;
        RaycastHit hitinfo;

        if (Input.GetMouseButton(0)) {
            // currently dragging the mouse
            if (initialMousePos != Vector3.zero) {
                // update the square
                finalMousePos = Input.mousePosition;
                Rect selectionBox = new Rect(Mathf.Min(initialMousePos.x, finalMousePos.x), Mathf.Min(initialMousePos.y, finalMousePos.y), Mathf.Abs(initialMousePos.x - finalMousePos.x), Mathf.Abs(initialMousePos.y - finalMousePos.y));
                selectionboxImage.GetComponent<RectTransform>().position = new Vector2(selectionBox.x, selectionBox.y);
                selectionboxImage.GetComponent<RectTransform>().sizeDelta = new Vector2(selectionBox.width, selectionBox.height);

                foreach (Unit unit in allUnits) {
                    Vector3 screenCoords = Camera.main.WorldToScreenPoint(unit.transform.position);

                    if (selectionBox.Contains(screenCoords)) {
                        // Check if we're allowed to select the unit. If we are, add it to our list
                        if (unit.Select()) {
                            selectedUnits.Add(unit);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            theray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(theray, out hitinfo)) {
                if (hitinfo.transform.gameObject.tag == "Node") {
                    foreach (Unit unit in selectedUnits) {
                        unit.AddTarget(hitinfo.transform.gameObject.transform);
                    }
                    hitinfo.transform.GetComponent<Node>().ToggleSelected();
                } else {
                    selectionboxCanvas.enabled = true;
                    initialMousePos = Input.mousePosition;
                }
            }
        } else if (Input.GetMouseButtonUp(0)) {
            selectionboxImage.GetComponent<RectTransform>().position = Vector2.zero;
            selectionboxImage.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            selectionboxCanvas.enabled = false;
        }
	}
}