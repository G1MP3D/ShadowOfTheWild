using UnityEngine;
using System.Collections;

public class basicMovment : MonoBehaviour {
	public float speed = 1;
	public float RotationSpeed = 50.0f;
	public float damp = 1;
	public float jumpforceUp = 1;
	public float jumpforceForward = 1;
	public float RayDistance =1;
	public float idleTimer = 12;
	public float idleCounter = 0;
	public GameObject[] tutorialIcons;
	bool Tutorial = true;
	float iconFadeTimer = 10;
	float fadeCounter = 0;

	bool grounded = true;
	Animator anim;

	// Use this for initialization
	void Awake () {
		tutorialIcons = GameObject.FindGameObjectsWithTag ("tutorial");

		anim =  transform.FindChild("character").GetComponent<Animator> ();
		anim.SetBool ("sitting", true);
	}
	void Start()
	{
		foreach(GameObject icon in tutorialIcons)
		{
			Debug.Log (icon.name);
			icon.SetActive(false);
		}
		Tutorial = true;
	}
	void OnEnable()
	{
		if(Tutorial)
		{
			tutorialIcons [1].SetActive (true);
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Tutorial)
		{
			if(tutorialIcons[1].activeInHierarchy)
			{
				if(fadeCounter<iconFadeTimer)
				{
					fadeCounter +=Time.deltaTime;
				}
				else
				{
					tutorialIcons[1].GetComponent<FadeAlpha>().StartFade( new Vector2(5,-1));
					fadeCounter = 0;

				}
			}
			else
			{
				tutorialIcons[0].SetActive(true);
				if(fadeCounter<iconFadeTimer)
				{
					fadeCounter +=Time.deltaTime;
				}
				else
				{
					tutorialIcons[0].GetComponent<FadeAlpha>().StartFade( new Vector2(5,-1));
					fadeCounter = 0;
					Tutorial = false;
				}
			}
		}
		float hInput = Input.GetAxis ("Horizontal");
		float vInput = Input.GetAxis ("Vertical");
        //float hInputRight = Input.GetAxis ("JoyStickHorizontalRight");
        //float vInputRight = Input.GetAxis ("JoyStickVerticalRight");

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
			anim.SetBool("isWalking",false);
		}
		else
		{
			anim.SetBool("isWalking",true);
		}
		 
		Vector3 moveDirection = new Vector3 (hInput, 0, vInput);
		//moveDirection.y = 0f;
		moveDirection = moveDirection.normalized;
		//Debug.Log (hInput + "  " + vInput +"   move" + moveDirection);
//		if (moveDirection != Vector3.zero) 
//		{
//			transform.rotation = Quaternion.LookRotation (moveDirection);
//		}

        //if(vInputRight<0)
        //{
        //    //Debug.Log (hInputRight + "hinputRight, " +hInputRight +1*3* RotationSpeed*Time.deltaTime );
        //        transform.Rotate (0, hInputRight *2* RotationSpeed*Time.deltaTime ,0);
        //}
        //else
        //{
        //    transform.Rotate (0, hInputRight * RotationSpeed*Time.deltaTime ,0);
        //}


//				if(hInput >0.18 || vInput >0.18)
//				{
//					transform.Translate(0,0,speed*((hInput+vInput)/2)*Time.deltaTime);
//				}
//		if(vInputRight !=0 || hInputRight!=0)
//		{
//			transform.eulerAngles =new Vector3 (transform.eulerAngles.x, Mathf.Atan2 (hInputRight, -vInputRight) * Mathf.Rad2Deg, transform.eulerAngles.z);
//		}
		if (grounded) // if the capsule is on the ground 
		{ 
			// when the space key is pressed the capsule should jump 
			if (Input.GetButton("Jump")) 
			{ 
				anim.SetTrigger("jump");
				grounded = false; // set grounded to false 
				// apply a relative force to the capsule up and in the direction the capsule is currently moving.  
				gameObject.GetComponent<Rigidbody>().AddRelativeForce(0, jumpforceUp, vInput * jumpforceForward); 
			} 
			
			// move the capsule on the z axis at the rate determined by moveVertical * speed 
			transform.Translate(Vector3.forward*speed*((vInput + Mathf.Abs(hInput))/2)*Time.deltaTime );
			
			// rotate the capsule around the y axis at the rate determined by moveHorizontal * rotationSpeed) 
		}
		Vector3 vDown = transform.TransformDirection(Vector3.down); 
		if (Physics.Raycast(transform.position, vDown,RayDistance)) 
		{ 
			grounded = true; 
		}
		if(vInput ==0&& hInput ==0 /*&& vInputRight ==0 && hInputRight ==0*/)
		{
			idleCounter+=Time.deltaTime;
			if(idleCounter>idleTimer)
			{
				anim.SetTrigger("sitDown");
			}
		}
		else
		{
			anim.SetBool("sitting",false);
			idleCounter =0;
		}
		
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
	void CrystalCollected()
	{

	}
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag=="crystal")
		{

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
			//Other.gameObject.GetComponent<Follower>().speed = 0;
		}
		if(Other.tag =="enemyZone")
		{
			Other.transform.GetChild(0).GetComponent<Follower>().speed =3 ;
		}
	}
	void OntriggerExit(Collider other)
	{
		if(other.tag=="enemyZone")
		{
			other.transform.GetChild(0).GetComponent<Follower>().speed = 0;
		}
	}
	
	
}