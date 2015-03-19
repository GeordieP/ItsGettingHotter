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

    // Nodes
    private Transform currentTarget;
    private List<Transform> targetNodes;
    private bool foundTarget = false;
    public Transform homeNode;

    // Task
    private UnitTask currentTask;

	// Resource
	private ResourcePackage carriedResource;

    // Unit
    public bool selected = false;
    public bool selectable = true;          // whether or not we can select this unit (used to avoid selecting units through the other side of the planet)
    private float unitRadius = 0.0f;

    // Unit Movement
    private Vector3 moveAxis = Vector3.zero;
    private float speed = 4.0f;
    private float currentTaskTime = 0.0f;

    // Use this for initialization
    void Start() {
        targetNodes = new List<Transform>();
        this.GetComponent<Renderer>().material.color = Color.blue;

        unitRadius = this.GetComponent<SphereCollider>().radius;
        //SnapToPlanetSurface();
    }

	void Update () {
        switch (state) {
            case States.Idle:
                //this.renderer.material.color = idleColor;
                this.GetComponent<Renderer>().material.color = (selected) ? Color.blue : Color.white;
                break;
            case States.Walking:
                if (!foundTarget) {
                    this.GetComponent<Renderer>().material.color = Color.blue;
                    transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
                } else {
                    currentTask = currentTarget.GetComponent<Node>().task;
                    //currentTaskTime = currentTarget.GetComponent<Node>().taskTime;      // time it takes to do a task is determined by the node itself for now - later it will also be influenced by the properties of the unit (movespeed taskspeed etc)
                    StartCoroutine(ExecuteTask());
                    ChangeState(States.Working);
                }
                break;
            case States.Working:
                break;
            default:
                break;
        }

        //this.GetComponent<Renderer>().material.color = Color.blue;
	}

    IEnumerator ExecuteTask() {
        this.GetComponent<Renderer>().material.color = Color.red;
		// Wait until the task time is up
        yield return new WaitForSeconds(currentTask.taskTime);
		// Task time has passed, 

		if (currentTarget == homeNode) {
			// we're at the home node, deposit the resources we're carrying
			// make sure we're not carrying nothing at some point, maybe not here but in the home node's intake function
			currentTarget.GetComponent<Node>().TaskCompleted(currentTarget == homeNode);	// we can just probably pass in our carriedResource here (make sure to null check)
			currentTarget.GetComponent<HomeNode>().AcceptResources(carriedResource);		// TODO: when we drop off resources, what should we do with what the unit is carrying?
		} else {
			// we're not at a home node, so gather as normal
			// probably need a condition for Build nodes as well

			// set our carried ResourcePackage to what was returned from the node when we call TaskCompleted
			carriedResource = currentTarget.GetComponent<Node>().TaskCompleted();
			//print("Picked up " + carriedResource.ResourceCount + " " + carriedResource.resourceType);
		}
        NextTarget();
    }

    private void NextTarget() {
        ChangeState(States.Idle);
        targetNodes.RemoveAt(0);
        foundTarget = false;
        if (targetNodes.Count > 0) {
            SetTarget(targetNodes[0]);
            ChangeState(States.Walking);
        } else {
            // no more nodes left in the list
            // return to home node
            AddTarget(homeNode);
        }
    }

    public void AddTarget(Transform _target) {
        targetNodes.Add(_target);

        if (state == States.Idle) {
            SetTarget(targetNodes[0]);
            ChangeState(States.Walking);
        }
    }

    public void SetTarget(Transform _target) {
        currentTarget = _target;
        moveAxis = Vector3.Cross(this.transform.position, currentTarget.position);

        if (state == States.Idle) {
            ChangeState(States.Walking);
        }
    }

    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.gameObject.transform == currentTarget) {
            // we've made it to the target
            foundTarget = true;
        }
    }

    /* Called by the camera when selecting units. Prevents selecting units that shouldn't be
        selectable gets updated by the camera and should only be true when the unit is visible to the player */
    public bool Select() {
        transform.GetComponent<Renderer>().material.color = Color.blue;
        selected = true;
        return true;
    }

    public void Deselect() {
        selected = false;
    }

    // Helper function for state changing so in the future we can make things happen when the state is changed if we need to (also for debugging)
    void ChangeState(States state) {
        this.state = state;
    }
}