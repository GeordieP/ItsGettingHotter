using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {

	public void Load()
	{
		Application.LoadLevel ("Main");
	}

	public void Restart()
	{
		Application.LoadLevel ("Main");
	}

	public void Exit()
	{
		Application.LoadLevel ("build");
	}
}
