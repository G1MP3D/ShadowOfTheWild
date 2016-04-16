using UnityEngine;
using System.Collections;

public class enemyZOne : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<AudioSource>().enabled=false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="PlayerCollider")
		{
			Debug.Log("PLayerNearEnemy");
			transform.GetChild(0).GetComponent<Follower>().speed = 3;
			gameObject.GetComponent<AudioSource>().enabled=true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.tag=="PlayerCollider")
		{
			transform.GetChild(0).GetComponent<Follower>().speed =0;
			gameObject.GetComponent<AudioSource>().enabled=false;
		}
	}
}
