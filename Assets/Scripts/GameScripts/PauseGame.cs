using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {
	public GameObject player;
	public GameObject enemy;
	public bool Paused = false;

	void Start()
	{

	}
	// Use this for initialization
	void OnEnable () {
		if (Paused)
		{
			player.SetActive (false);
			enemy.SetActive (false);
		}
	}
	void OnDisable()
	{
		player.SetActive (true);
		enemy.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
