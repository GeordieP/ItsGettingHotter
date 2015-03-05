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

        //selectedUnits.AddRange(allUnits);

        selectionboxCanvas.enabled = false;
        selectionboxImage = selectionboxCanvas.transform.FindChild("Panel").gameObject;
    }

	// Update is called once per frame
	void Update () {
        Ray theray;
        RaycastHit hitinfo;


        // TODO: Combine these two into one if statement for MouseButton 0

        // Handle unit selection
        if (Input.GetMouseButton(0)) {
            // currently dragging the mouse
            if (initialMousePos != Vector3.zero) {
                // update the square
                finalMousePos = Input.mousePosition;
                Rect selectionBox = new Rect(Mathf.Min(initialMousePos.x, finalMousePos.x), Mathf.Min(initialMousePos.y, finalMousePos.y), Mathf.Abs(initialMousePos.x - finalMousePos.x), Mathf.Abs(initialMousePos.y - finalMousePos.y));
                selectionboxImage.GetComponent<RectTransform>().position = new Vector2(selectionBox.x, selectionBox.y);
                selectionboxImage.GetComponent<RectTransform>().sizeDelta = new Vector2(selectionBox.width, selectionBox.height);

                foreach (Unit unit in allUnits) {
                    // deselect the current unit before anything. this should prevent old units staying selected when we want to select a new bunch
                    unit.Deselect();

                    Vector3 screenCoords = Camera.main.WorldToScreenPoint(unit.transform.position);


                    // TODO: the problem is here - unit gets added once per frame if it's in the list
                    // for now we can just do a shitty check and see if it's already in there
                    // but a more elegant solution is needed

                    if (selectionBox.Contains(screenCoords)) {
                        // Check if we're allowed to select the unit by calling unit.Select(). If we are, add it to our list (unit.Select() will also update the unit's selected status)
                        if (unit.Select()) {
                            if (!selectedUnits.Contains(unit)) {
                                //print("adding unit");
                                selectedUnits.Add(unit);
                            }
                        }
                    }
                }
            }
        }

        // Handle node selection
        if (Input.GetMouseButtonDown(0)) {
            //print("ye");
            theray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(theray, out hitinfo)) {
                if (hitinfo.transform.gameObject.tag == "Node") {

                    foreach (Unit unit in selectedUnits) {
                        unit.AddTarget(hitinfo.transform.gameObject.transform);
                    }

                    //foreach (Unit unit in allUnits) {
                    //    unit.AddTarget(hitinfo.transform.gameObject.transform);
                    //}

                    hitinfo.transform.GetComponent<Node>().ToggleSelected();
                } else {
                    selectionboxCanvas.enabled = true;
                    initialMousePos = Input.mousePosition;

                    // clear selected units list
                    selectedUnits.Clear();
                }
            }
        } else if (Input.GetMouseButtonUp(0)) {
            selectionboxImage.GetComponent<RectTransform>().position = Vector2.zero;
            selectionboxImage.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            selectionboxCanvas.enabled = false;
        }
	}
}