using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {
	public float rotateSpeed;
	public float BobDistance;
	float Bobcenter;
	bool collected = false;
	public float DestroyTimer = 2;
	float timerCounter = 0;
	public float BobSpeed;
	public float damp;
	public float newSpeed;
	float timeStamp = 0;



	// Use this for initialization
	void Start () {
		Bobcenter = gameObject.transform.position.y;
		newSpeed = BobSpeed;
		transform.eulerAngles = new Vector3 (0, Random.Range(0,360), 0);

	}
	
	// Update is called once per frame
	void Update () {
		if(collected)
		{
			timerCounter +=Time.deltaTime;
			if(timerCounter>DestroyTimer)
			{
				gameObject.SetActive(false);
			}
		}
		transform.Rotate (0, rotateSpeed*Time.deltaTime, 0);
		if(transform.position.y > Bobcenter+BobDistance)
		{
			newSpeed*=-1;
		}
		if(transform.position.y < Bobcenter-BobDistance)
		{
			newSpeed*=-1;
		}
		if( transform.position.y == Bobcenter)
		{
			damp*=-1;
		}
	
		transform.Translate (new Vector3 (0, newSpeed * Time.deltaTime, 0));
		if(Time.time - timeStamp >=.5)
		{
			timeStamp =Time.time;
			newSpeed += damp;
		}

	}
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag=="Player")
		{
			Debug.Log("CollidedWithPlayer");
			if(collected==false)
			{
				collected=true;
				other.transform.root.gameObject.SendMessage("CollectedItem",1);
				gameObject.GetComponent<Rigidbody>().useGravity = true;
				gameObject.transform.GetChild(0).SendMessage("StartFade",new Vector2(3,-1));
				transform.GetChild(1).GetComponent<AudioSource>().Play();
			}
		}
	}
}
