using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

    public float taskTime = 0.5f;       // seconds

    private bool selected = false;

    void Start() {
        //SnapToPlanetSurface();
    }

    public void ToggleSelected() {
        selected = !selected;
        //UpdateColor();
    }

    public void TaskCompleted() {
        selected = false;
        this.transform.gameObject.SetActive(false);
        //UpdateColor();
    }

    public void TaskCompleted(bool isHomeNode) {
        selected = false;
        if (!isHomeNode) this.transform.gameObject.SetActive(false);
        //UpdateColor();
    }

    private void UpdateColor() {
        this.GetComponent<Renderer>().material.color = (selected) ? Color.green : Color.white;
    }

    //private void SnapToPlanetSurface() {
    //    Ray theray = new Ray(this.transform.position, (planet.transform.position - this.transform.position).normalized);
    //    RaycastHit hit;

    //    if (Physics.Raycast(theray, out hit)) {
    //        transform.position = Vector3.MoveTowards(this.transform.position, hit.point, hit.distance);
    //        transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
    //    }
    //}
}
