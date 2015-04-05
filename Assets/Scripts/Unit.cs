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
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.T)) {
            print("State: " + state);
        }

		switch (state) {
			case States.Idle:
				//this.renderer.material.color = idleColor;
				this.GetComponent<Renderer>().material.color = (selected) ? Color.blue : Color.white;
				//this.GetComponent<Renderer>().material.color = Color.green;
				break;
			case States.Walking:
				if (!foundTarget) {
					this.GetComponent<Renderer>().material.color = Color.blue;
					transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
				} else {
					ChangeState(States.Working);
					StartTask();
				}
				break;
			case States.Working:
				this.GetComponent<Renderer>().material.color = Color.red;
				break;
			default:
				break;
		}

		this.GetComponent<Renderer>().material.color = Color.blue;
	}

	// Begin executing the task provided by the node we're at
	void StartTask() {
		// TODO: change this when Node and HomeNode are set up a bit better!
		currentTask = currentTarget.GetComponent<Node>().GetTask();
		StartCoroutine(WaitForTaskTime());
	}

	// Wait for some time before completing the task (this is the "work" period)
	IEnumerator WaitForTaskTime() {
		yield return new WaitForSeconds(currentTask.taskTime);
		TaskCompleted();
	}

	// Work period has gone by, now we can call TaskCompleted on our current task, and move on to the next target in the queue
	public void TaskCompleted() {
		currentTask.TaskCompleted(this, currentTarget.GetComponent<Node>());
		NextTarget();
	}

	// Accept a node's ResourcePackage -- this should only ever get called from a GatherTask
	public void AcceptResourcePackage(ResourcePackage _resourcePackage) {
		carriedResource = _resourcePackage;
	}

	// Take our carried ResourcePackage, so the UnitTask can give it to the city -- should only ever get called from a DepositTask
	public ResourcePackage TakeResourcePackage() {
		return carriedResource;
	}

    private void NextTarget() {
        ChangeState(States.Idle);
        foundTarget = false;
        targetNodes.RemoveAt(0);

        if (targetNodes.Count > 0) {
            SetTarget(targetNodes[0]);
            ChangeState(States.Walking);
        } else {
            // no more nodes left in the list
            // return to home node
            //AddTarget(homeNode);
        }
    }

    public void AddTarget(Transform _target) {
        // HACK: this is a shit way to do this

		// only add the target if the queue is empty, or the target isn't the same as the last node that was added
		if (targetNodes.Count < 1 || _target != targetNodes[targetNodes.Count - 1]) {
			targetNodes.Add(_target);
		}

		//if (targetNodes.Count < 1) {
		//	targetNodes.Add(_target);
		//} else if (_target != targetNodes[targetNodes.Count - 1]) {

		//}

		//if (targetNodes.Count > 0 && _target != targetNodes[targetNodes.Count-1]) {
		//	targetNodes.Add(_target);

		//}

		//if (!targetNodes.Contains(_target)) {
		//	targetNodes.Add(_target);
		//}

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
