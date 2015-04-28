using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class StartButton : MonoBehaviour {

	public EventSystem eventSystem;
	public void LoadGame()
	{
		Application.LoadLevel (1);
	}
	public void RestartGame()
	{
		Application.LoadLevel (1);
	}
	public void LoadMainMenu()
	{
		Time.timeScale = 1;
		Application.LoadLevel (0);
	}
	public void ResumeGame()
	{
		GameObject menu = GameObject.Find ("Main Camera");
		MenuOpen MO = menu.GetComponent<MenuOpen> ();
		MO.ResumeGame ();
	}
}
