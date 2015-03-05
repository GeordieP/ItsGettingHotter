/*
 * Created by Geordie Powers
 * Feb 5 2015
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Unit : MonoBehaviour {

    // States
    private States state = States.Idle;
    private enum States {
        Idle,
        Walking,
        Working
    }

    // Planet
    public GameObject planet;
    
    // Nodes
    private Transform currentTarget;
    private List<Transform> targetNodes;
    private bool foundTarget = false;

    // Unit
    Color idleColor = Color.white;
    public bool selected = false;
    public bool selectable = false;          // whether or not we can select this unit (used to avoid selecting units through the other side of the planet)
    private float unitRadius = 0.0f;

    // Unit Movement
    private Vector3 moveAxis = Vector3.zero;
    private float speed = 10.3f;
    private float currentTaskTime = 0.0f;

	// Use this for initialization
	void Start () {
        targetNodes = new List<Transform>();

        unitRadius = this.GetComponent<SphereCollider>().radius;
        //unitRadius = this.transform.localScale.x;

        planet = GameObject.Find("Planet");     // TODO: replace this line when the global class is implemented, as it should have a reference to the planet at all times

        SnapToPlanetSurface();
	}

	// Update is called once per frame
	void Update () {
        switch (state) {
            case States.Idle:
                //this.renderer.material.color = idleColor;
                this.GetComponent<Renderer>().material.color = (selected) ? Color.blue : Color.white;
                break;
            case States.Walking:
                if (!foundTarget) {
                    this.GetComponent<Renderer>().material.color = Color.blue;
                    transform.RotateAround(planet.transform.position, moveAxis, speed * Time.deltaTime);
                } else {
                    currentTaskTime = currentTarget.GetComponent<Node>().taskTime;      // time it takes to do a task is determined by the node itself for now - later it will also be influenced by the properties of the unit (movespeed taskspeed etc)
                    StartCoroutine(ExecuteTask());
                    state = States.Working;
                }
                break;
            case States.Working:
                break;
            default:
                break;
        }
	}

    IEnumerator ExecuteTask() {
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(currentTaskTime);
        // task is completed, move on
        currentTarget.GetComponent<Node>().TaskCompleted();
        NextTarget();
    }

    private void NextTarget() {
        state = States.Idle;
        targetNodes.RemoveAt(0);
        foundTarget = false;
        if (targetNodes.Count > 0) {
            SetTarget(targetNodes[0]);
            state = States.Walking;
        } else {
            // no more nodes left in the list
            // return to home node?
        }
    }

    public void AddTarget(Transform _target) {
        //print("adding  target");
        targetNodes.Add(_target);

        if (state == States.Idle) {
            SetTarget(targetNodes[0]);
            state = States.Walking;
        }
    }

    public void SetTarget(Transform _target) {
        currentTarget = _target;
        moveAxis = Vector3.Cross(this.transform.position, currentTarget.position);

        if (state == States.Idle) {
            state = States.Walking;
        }
    }

    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.gameObject.transform == currentTarget) {
            // we've made it to the target
            foundTarget = true;
        }
    }

    public void SnapToPlanetSurface() {
        Ray theray = new Ray(this.transform.position, (planet.transform.position - this.transform.position).normalized);
        RaycastHit hit;

        if (Physics.Raycast(theray, out hit)) {
            this.transform.position = Vector3.MoveTowards(this.transform.position, hit.point, hit.distance);
        }
    }

    // Called by the camera when selecting units. Prevents selecting units that shouldn't be
    // Selectable gets updated by the camera and should only be true when the unit is visible to the player
    public bool Select() {
        selected = selectable;
        return selectable;
    }

    public void Deselect() {
        selected = false;
    }
}
