using UnityEngine;
using System.Collections;

public class playerInWater : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Transform current = player.GetComponent<Switch> ().current.transform;
		if(gameObject.GetComponent<Collider>().bounds.Contains(current.position))
		  {
			player.SendMessage("InWater",true);
		  }
	}
}
