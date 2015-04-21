using UnityEngine;
using System.Collections;

public class MenuOpen : MonoBehaviour {
	public GameObject canvs;
	public bool isActive = false;
	public bool isAlive = true;
	// Use this for initialization
	void Start () {
		isAlive = true;
		canvs.SetActive (false);
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(isAlive)
		{
			if(Input.GetKeyDown(KeyCode.Escape) && isActive == false)
			{
				PauseGame();
			}
			else if(Input.GetKeyDown(KeyCode.Escape) && isActive == true)
			{
				ResumeGame();
			}
		}
	}

	public void ResumeGame()
	{
		Time.timeScale = 1;
		isActive = false;
		canvs.SetActive(false);
	}
	public void PauseGame()
	{
		Time.timeScale = 0;
		isActive = true;
		canvs.SetActive(true);
	}
	public void EndGame()
	{
		isAlive = false;
		canvs.transform.GetChild (0).transform.GetChild (0).gameObject.SetActive (false);
		Time.timeScale = 0;
		isActive = true;
		canvs.SetActive(true);
	}
}
