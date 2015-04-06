using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickHandling : MonoBehaviour {

    public Unit[] allUnits;
    public List<Unit> selectedUnits;

    private Vector3 iMousePos = Vector3.zero;
    private Vector3 fMousePos = Vector3.zero;

    public Canvas selectionBoxCanvas;
    private GameObject selectionBoxImage;

	// Use this for initialization
	void Start () {
        RefreshAllUnitsList();
        selectedUnits = new List<Unit>();

        selectionBoxCanvas.enabled = false;
        selectionBoxImage = selectionBoxCanvas.transform.FindChild("Panel").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Ray theray;
        RaycastHit hitinfo;

        theray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0)) {
            iMousePos = Input.mousePosition;

            if (Physics.Raycast(theray, out hitinfo)) {
                if (hitinfo.transform.gameObject.tag == "Node") {
                    //print("selected units length: " + selectedUnits.Count);
                    foreach (Unit unit in selectedUnits) {
                        unit.AddTarget(hitinfo.transform);
                    }

                    hitinfo.transform.GetComponent<Node>().ToggleSelected();
                } else {
                    selectedUnits.Clear();
                }
            }
        }

        if (Input.GetMouseButton(0)) {
            fMousePos = Input.mousePosition;
            Rect selectionBox = new Rect(Mathf.Min(iMousePos.x, fMousePos.x), Mathf.Min(iMousePos.y, fMousePos.y), Mathf.Abs(iMousePos.x - fMousePos.x), Mathf.Abs(iMousePos.y - fMousePos.y));

            selectionBoxImage.GetComponent<RectTransform>().position = new Vector2(selectionBox.x, selectionBox.y);
            selectionBoxImage.GetComponent<RectTransform>().sizeDelta = new Vector2(selectionBox.width, selectionBox.height);
            selectionBoxCanvas.enabled = true;
            foreach (Unit unit in allUnits) {
                unit.Deselect();
                Vector3 screenCoords = Camera.main.WorldToScreenPoint(unit.transform.position);
                if (selectionBox.Contains(screenCoords)) {
                    if (unit.Select()) {
                        if (!selectedUnits.Contains(unit)) {
                            selectedUnits.Add(unit);
                        }
                    }
                }
            }
        } 
        
        if (Input.GetMouseButtonUp(0)) {
            selectionBoxCanvas.enabled = false;
            selectionBoxImage.GetComponent<RectTransform>().position = Vector2.zero;
            selectionBoxImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            selectionBoxImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        }
	}

    public void RefreshAllUnitsList() {
        allUnits = GameObject.FindObjectsOfType<Unit>();
    }
}
