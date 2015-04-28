using UnityEngine;
using System.Collections;

public class MenuOpen : MonoBehaviour {
	public GameObject canvs;
	public bool isActive = false;
	public bool isAlive = true;
    public AudioClip resume;
    public AudioClip pause;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
		isAlive = true;
		canvs.SetActive (false);
		Time.timeScale = 1;
        audio = gameObject.GetComponent<AudioSource>();
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
        audio.PlayOneShot(resume);
	}
	public void PauseGame()
	{
		Time.timeScale = 0;
		isActive = true;
		canvs.SetActive(true);
        audio.PlayOneShot(pause);
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
