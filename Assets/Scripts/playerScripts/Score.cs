using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
	public int PlayerScore = 0;
	public Text ScoreText;
	GameObject[] Crystals;
	// Use this for initialization
	void Start () {
		PlayerScore = 0;
		Crystals = GameObject.FindGameObjectsWithTag("Points");
	}
	
	// Update is called once per frame
	void Update () {
		ScoreText.text = "Crystals " + PlayerScore + "/" + Crystals.Length;
	
	}
	void CollectedItem(int points)
	{
		PlayerScore += 1;
	}
}
