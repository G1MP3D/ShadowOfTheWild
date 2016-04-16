using UnityEngine;
using System.Collections;

public class deerController : MonoBehaviour {

	public float speed = 1;
	public float RotationSpeed = 50.0f;
	public float damp = 1;
	Animator anim;
	AudioSource Audio;
	// Use this for initialization
	void Start () {
		anim =  transform.FindChild("deer").GetComponent<Animator> ();
		Audio = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float hInput = Input.GetAxis ("JoyStickHorizontal");
		float vInput = Input.GetAxis ("JoyStickVertical");
		float hInputRight = Input.GetAxis ("JoyStickHorizontalRight");
		float vInputRight = Input.GetAxis ("JoyStickVerticalRight");
		
		if(hInput<0.18 && hInput>0)
		{
			hInput = 0;
		}
		if(vInput<0.18 && vInput>0)
		{
			vInput = 0;
		}
		if(vInput==0 && hInput ==0)
		{
			anim.SetBool("run",false);
			Audio.Pause();
		}
		else
		{
			anim.SetBool("run",true);
			Audio.Play();

		}
		
		Vector3 moveDirection = new Vector3 (hInput, 0, vInput);
		moveDirection.y = 0f;
		moveDirection = moveDirection.normalized;
		//Debug.Log (hInput + "  " + vInput +"   move" + moveDirection);
		//		if (moveDirection != Vector3.zero) 
		//		{
		//			transform.rotation = Quaternion.LookRotation (moveDirection);
		//		}
		
		if(vInputRight<0)
		{
			//Debug.Log (hInputRight + "hinputRight, " +hInputRight +1*3* RotationSpeed*Time.deltaTime );
			transform.Rotate (0, hInputRight *2* RotationSpeed*Time.deltaTime ,0);
		}
		else
		{
			transform.Rotate (0, hInputRight * RotationSpeed*Time.deltaTime ,0);
		}
		
		transform.Translate(Vector3.forward*speed*((vInput + Mathf.Abs(hInput))/2)*Time.deltaTime );
		//				if(hInput >0.18 || vInput >0.18)
		//				{
		//					transform.Translate(0,0,speed*((hInput+vInput)/2)*Time.deltaTime);
		//				}
		//		if(vInputRight !=0 || hInputRight!=0)
		//		{
		//			transform.eulerAngles =new Vector3 (transform.eulerAngles.x, Mathf.Atan2 (hInputRight, -vInputRight) * Mathf.Rad2Deg, transform.eulerAngles.z);
		//		}
		
		
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
	void OnTriggerExit(Collider other)
	{
		if(other.tag=="enemyZone")
		{
			other.transform.GetChild(0).GetComponent<Follower>().speed = 0;
		}
	}
	
	
}