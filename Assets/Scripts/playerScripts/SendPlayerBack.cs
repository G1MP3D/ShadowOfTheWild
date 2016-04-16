using UnityEngine;
using System.Collections;

public class SendPlayerBack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="PlayerCollider")
		{
			Debug.Log("PLayerHitEnemy");
			other.transform.root.SendMessage("HitWater");
			gameObject.GetComponent<Follower>().speed = 0;
		}
	}

}
