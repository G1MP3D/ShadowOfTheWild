using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

	public Transform target;
	public float speed;
	public float smooth;
	public Transform player;
	public Transform head;
	public Vector3 headOffset;
	void Awake()
	{

	}
	void FixedUpdate () 
	{


			target = player.GetComponent<Switch> ().current.transform;
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			Vector3 relativePos = target.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation=new Quaternion(0, rotation.y,0,transform.rotation.w);
		if(speed!=0)
		{
			gameObject.GetComponent<AudioSource>().enabled=true;

		}
		else
		{
			gameObject.GetComponent<AudioSource>().enabled=false;
			transform.root.GetComponent<AudioSource>().enabled=false;
		}
			//transform.rotation = Quaternion.RotateTowards(Quaternion.rotation, Quaterniountarget.rotation,step);



	}
}
