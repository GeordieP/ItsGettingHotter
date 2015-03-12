using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour
{
    private float TotalTime;
    public float TimeLeftInMinutes;
    public float PollutionFactor;
    public float CH4; //Doesn't last as long, greater effect
    public float CO2; //Lasts longer, lesser effect
    private const float CHINFLUENCE = 0.05f;
    private const float COINFLUENCE = 0.025f;
    private float CoTime;
    private float ChTime;
    private Vector2 startSize;

	// Use this for initialization
	void Start ()
    {
        CH4 = 0;
        CO2 = 0;
        TimeLeftInMinutes *= 60; //converting to seconds because I have to
        TotalTime = TimeLeftInMinutes;
        ChTime = 0.025f * TotalTime;
        CoTime = 0.05f * TotalTime;
        startSize = transform.GetComponent<RectTransform>().sizeDelta;
	}
	
	// Update is called once per frame
	void Update ()
    {
        PollutionFactor = GetPollutionFactor();
        TimeLeftInMinutes -= Time.deltaTime + PollutionFactor;
        if (TimeLeftInMinutes <= 0)
        {
            Debug.LogError("Game Over!");
        }

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(startSize.x - (TotalTime - TimeLeftInMinutes), startSize.y);
	}

    private float GetPollutionFactor()
    {
        return (CH4 * CHINFLUENCE) + (CO2 * COINFLUENCE);
    }

    public void AddCo2()
    {
        CO2++;
    }
    public void AddCh4()
    {
        CH4++;
    }
}
