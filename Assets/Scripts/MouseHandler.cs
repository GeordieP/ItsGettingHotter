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
        selectionboxImage = selectionboxCanvas.transform.FindChild("Panel").gameObject;     // TODO: this seems like such a flaky way to access the image, maybe find something better
    }

	// Update is called once per frame
	void Update () {
        Ray theray;
        RaycastHit hitinfo;

        // Handle unit selection
        if (Input.GetMouseButton(0)) {
            // if user is dragging the mouse/has moved since first clicking
            if (Input.mousePosition != initialMousePos) {
                // update the mouse position, create the selection box
                finalMousePos = Input.mousePosition;
                Rect selectionBox = new Rect(Mathf.Min(initialMousePos.x, finalMousePos.x), Mathf.Min(initialMousePos.y, finalMousePos.y), Mathf.Abs(initialMousePos.x - finalMousePos.x), Mathf.Abs(initialMousePos.y - finalMousePos.y));
                // update the selection box image 
                selectionboxImage.GetComponent<RectTransform>().position = new Vector2(selectionBox.x, selectionBox.y);
                selectionboxImage.GetComponent<RectTransform>().sizeDelta = new Vector2(selectionBox.width, selectionBox.height);

                selectionboxCanvas.enabled = true;      // TODO: doing this every frame seems stupid, but it looks weird if we do it in GetMouseButtonDown

                foreach (Unit unit in allUnits) {
                    // deselect the current unit before anything. this should prevent old units staying selected when we want to select a new bunch
                    // TODO: should we do this inside GetMouseButtonDown instead? We'd need to loop through all units again redundantly, but the behavior would work entirely as intended
                    unit.Deselect();

                    Vector3 screenCoords = Camera.main.WorldToScreenPoint(unit.transform.position);

                    /* TODO: the movement/node task problem is here - unit gets added once per frame if it's in the selection box
                       for now we can just do a shitty check and see if it's already in the selected list
                       but a more elegant solution is needed */

                    if (selectionBox.Contains(screenCoords)) {
                        // Check if we're allowed to select the unit by calling unit.Select(). If we are, add it to our list (unit.Select() will also update the unit's selected status)
                        if (unit.Select()) {
                            if (!selectedUnits.Contains(unit)) {
                                selectedUnits.Add(unit);
                            }
                        }
                    }
                }
            }
        }

        // Handle node selection
        if (Input.GetMouseButtonDown(0)) {
            theray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(theray, out hitinfo)) {
                // if the player clicks on a node, and not an unobstructed place on the planet
                if (hitinfo.transform.gameObject.tag == "Node") {

                    foreach (Unit unit in selectedUnits) {
                        unit.AddTarget(hitinfo.transform.gameObject.transform);
                    }

                    hitinfo.transform.GetComponent<Node>().ToggleSelected();
                } else {
                    initialMousePos = Input.mousePosition;

                    // clear selected units list
                    selectedUnits.Clear();
                }
            }
        } else if (Input.GetMouseButtonUp(0)) {
            selectionboxImage.GetComponent<RectTransform>().position = Vector2.zero;
            selectionboxImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            selectionboxImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            selectionboxCanvas.enabled = false;
        }
	}
}