using UnityEngine;
using System.Collections;

public class BirdControlls : MonoBehaviour {

	public float speed = 0.1f;
	public float RotationSpeed = 50.0f;
	public float damp = 1;
	public float maxAngle = 30;
	Animator anim;
	AudioSource Audio;
	void Awake()
	{
		anim =  transform.FindChild("bird").GetComponent<Animator> ();
		Audio = gameObject.GetComponent<AudioSource> ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		int rand = Random.Range (1, 100);
		if(rand ==1)
		{
			Audio.Play();
		}
		float hInput = Input.GetAxis ("JoyStickHorizontal");
		float vInput = Input.GetAxis ("JoyStickVertical");
		//float hInputRight = Input.GetAxis ("JoyStickHorizontalRight");
		//float vInputRight = Input.GetAxis ("JoyStickVerticalRight");

		anim.SetFloat("direction",hInput);

		if(hInput<0.18 && hInput>0)
		{
			hInput = 0;
		}
		if(vInput<0.18 && vInput>0)
		{
			vInput = 0;
		}


		transform.Translate(Vector3.forward*speed*Time.deltaTime );
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
		transform.Rotate (vInput*maxAngle*Time.deltaTime, hInput * RotationSpeed*Time.deltaTime ,0);
		
		
	}
	void EnterWater(bool yesNo)
	{

		int i=0;
		GameObject[] WaterBodies = GameObject.FindGameObjectsWithTag ("Water");
		foreach( GameObject water in WaterBodies)
		{
			if(water.GetComponent<Collider>().bounds.Contains(transform.position))
			{
				i+=1;
			}
		}
		if(i>0)
		{
			transform.root.gameObject.SendMessage ("EnterWater");
		}
		
	}
	void OnTriggerEnter(Collider Other)
	{
		if(Other.tag=="Water")
		{
			transform.root.gameObject.SendMessage ("HitWater");
		}
		if(Other.tag=="enemy")
		{
			transform.root.SendMessage("HitWater");
			Other.gameObject.GetComponent<Follower>().speed = 0;
		}
		if(Other.tag =="enemyZone")
		{
			Other.transform.GetChild(0).GetComponent<Follower>().speed =3 ;
		}
	}
}
