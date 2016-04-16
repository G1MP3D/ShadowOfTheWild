using UnityEngine;
using System.Collections;

public class fishController : MonoBehaviour {

	public float speed = 0.1f;
	public float RotationSpeed = 50.0f;
	public float damp = 1;
	public float maxAngle = 30;
	public float Range = 0.1f;
	public bool inWater;
	Animator anim;
	Transform ColliderTransform; 
	AudioSource Audio;
	void Awake()
	{
		Audio = gameObject.GetComponent<AudioSource> ();
		anim =  transform.FindChild("fish").GetComponent<Animator> ();
		ColliderTransform = gameObject.transform.FindChild("collider");
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float hInput = Input.GetAxis ("JoyStickHorizontal");
		float vInput = Input.GetAxis ("JoyStickVertical");
		float hInputRight = Input.GetAxis ("JoyStickHorizontalRight");
		float vInputRight = Input.GetAxis ("JoyStickVerticalRight");
		Debug.DrawRay(transform.position, Vector3.down * 50, Color.green);
		
		
		if(hInput<0.18 && hInput>0)
		{
			hInput = 0;
		}
		if(vInput<0.18 && vInput>0)
		{
			vInput = 0;
		}
		if(hInputRight<0.18 && hInputRight>0)
		{
			hInputRight = 0;
		}
		if(vInputRight<0.18 && vInputRight>0)
		{
			vInputRight = 0;
		}
		

		//Vector3 pitch = new Vector3 (0, transform.localEulerAngles.y, transform.localEulerAngles.z);
		
		

		
		
		//		if(vInput==0)
		//		{
		//			if(transform.rotation.x>0.01 ||transform.rotation.x<-0.01)
		//			{
		//				if (transform.rotation.x>180||transform.rotation.x<0)
		//				{
		//					transform.Rotate (0.2f*maxAngle*Time.deltaTime,0,0);
		//				}
		//				else
		//				{
		//					transform.Rotate (-0.2f*maxAngle*Time.deltaTime,0,0);
		//				}
		//			}
		//			else
		//			{
		//				transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,transform.eulerAngles.z);
		//			}
		//		}
		RaycastHit hit;
		if(inWater)
		{
			anim.SetBool("inWater",true);
			Audio.Play();
			if(hInput==0)
			{
				if(transform.rotation.z>0.01f ||transform.rotation.z<-0.01f)
				{
					if (transform.rotation.z>180 ||transform.rotation.z<0)
					{
						transform.Rotate (0,0,0.2f*maxAngle * Time.deltaTime);
					}
					else 
					{
						transform.Rotate (0,0,-0.2f*maxAngle * Time.deltaTime);
					}
				}
				else
				{
					transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,0);
				}
			}

			transform.Rotate (vInputRight*maxAngle*Time.deltaTime, hInputRight * RotationSpeed*Time.deltaTime ,0);

			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0);
			transform.Translate(Vector3.forward*speed*((Mathf.Abs(vInput) + Mathf.Abs(hInput))/2)*Time.deltaTime );
			anim.SetFloat("moving",vInput);
		}
		else if(Physics.Raycast (transform.position, Vector3.down, out hit, Range))
		{
			Audio.Pause();
			transform.Translate(speed/4 *Time.deltaTime,0,speed/4*Time.deltaTime);
			anim.SetBool("inWater",false);
			gameObject.GetComponent<Rigidbody>().useGravity = true;
			gameObject.GetComponent<Rigidbody>().isKinematic = false;
			//ColliderTransform.position = new Vector3(0.06050242f,ColliderTransform.localScale.y,ColliderTransform.localScale.z);

		}
		else
		{
			Audio.Pause();
			anim.SetBool("inWater",false);
			transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,-90);
			gameObject.GetComponent<Rigidbody>().useGravity = true;
			gameObject.GetComponent<Rigidbody>().isKinematic = false;
			//ColliderTransform.localScale = new Vector3(0.3f,ColliderTransform.localScale.y,ColliderTransform.localScale.z);
		}


		


	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Water")
		{
			inWater = true;
			transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
			gameObject.GetComponent<Rigidbody>().useGravity = false;
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
		if(other.tag=="enemy")
		{
			transform.root.SendMessage("HitWater");
			other.gameObject.GetComponent<Follower>().speed = 0;
		}
		if(other.tag =="enemyZone")
		{
			other.transform.GetChild(0).GetComponent<Follower>().speed =3 ;
		}
	}
	void EnterWater(bool yesNo)
	{

		inWater = yesNo;
		if(inWater)
		{
			transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
			gameObject.GetComponent<Rigidbody>().useGravity = false;
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
		else
		{
			inWater = false;
		}

	
	}
	void OnTriggerExit(Collider other)
	{
		if(other.tag=="Water")
		{
			inWater = false;
		}
		if(other.tag=="enemyZone")
		{
			other.transform.GetChild(0).GetComponent<Follower>().speed = 0;
		}
	}



}