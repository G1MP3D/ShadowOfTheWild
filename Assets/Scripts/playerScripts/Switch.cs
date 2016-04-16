using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
	public GameObject switchUp;
	public GameObject switchDown;
	public GameObject switchLeft;
	public GameObject switchRight;
	public GameObject current;
	public float TimeLimit = 0;
	public float CoolDownTimer = 0;
	bool coolDown =false;
	public float TimeCounter =0;
	public bool UpAvalable = false;
	public bool DownAvalable = false;
	public bool LeftAvalable = false;
	public bool RightAvalable = false;
	bool inWater =false;
	public float characterPosOffset =1f;
	GameObject playerCamera;
	GameObject[] WaterBodies;
	GameObject DeerIcon;
	GameObject BirdIcon;
	GameObject fishIcon;
	GameObject ArrowIcon;
	GameObject Gems;
	GameObject Gem1;
	GameObject Gem2;
	GameObject Gem3;
	GameObject Gem4;
	Vector3 CurrentArrowRotation = Vector3.zero;
	Vector3 StartRotation = Vector3.zero;
	Vector3 ArrowUp =new Vector3(0,0,146.3124f);
	Vector3 ArrowDown =new Vector3(0,0,321.8524f);
	Vector3 ArrowLeft = new Vector3(0,0,236.1188f);
	Vector3 ArrowRight= new Vector3(0,0,413.1094f);
	Vector2 CamPositionCharacter = new Vector2(5,1);
	Vector2 CamPositionDeer = new Vector2(2.56f,2.07f);
	Vector2 CamPositionFish = new Vector2(1.04f,-0.28f);
	Vector2 CamPositionBird = new Vector2(1.34f,0.98f);
	AudioSource Audio;
    void Awake ()
    {
    //    StartRotation = StartRotation;
    //    Audio = gameObject.GetComponent<AudioSource> ();
    //    DeerIcon = GameObject.Find ("DeerIcon");
    //    BirdIcon = GameObject.Find ("BirdIcon");
    //    fishIcon = GameObject.Find ("FishIcon");
    //    ArrowIcon = GameObject.Find ("ArrowIcon");
    //    Gems = GameObject.Find ("Gems");
    //    Gem1 = GameObject.Find ("Gem1");
    //    Gem2 = GameObject.Find ("Gem2");
    //    Gem3 = GameObject.Find ("Gem3");
    //    Gem4 = GameObject.Find ("Gem4");
    //    //Gems.SetActive (false);
    //    DeerIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
    //    BirdIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
    //    fishIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
    //    DeerIcon.SetActive (false);
    //    BirdIcon.SetActive (false);
    //    fishIcon.SetActive (false);
        ArrowIcon.SetActive (false);


    }

	void Start () {
		playerCamera =  GameObject.Find ("Main Camera");
		WaterBodies = GameObject.FindGameObjectsWithTag("Water");
		ArrowIcon.transform.eulerAngles = Vector3.MoveTowards(CurrentArrowRotation,ArrowDown,1);
		CurrentArrowRotation = ArrowIcon.transform.eulerAngles;
		playerCamera.GetComponent<playerCamScript2> ().distanceUp = CamPositionCharacter[1]; 
		playerCamera.GetComponent<playerCamScript2> ().distanceAway =  CamPositionCharacter[0]; 
	}
	void FixedUpdate()
	{
		ArrowIcon.transform.eulerAngles = Vector3.MoveTowards(ArrowIcon.transform.eulerAngles,CurrentArrowRotation,1);
	}
	// Update is called once per frame
	void Update () 
	{
		Debug.Log (Input.GetAxis ("DpadVertical") + "   " + Input.GetAxis ("DpadHorizontal"));

		if(switchDown.GetComponent<basicMovment>().enabled==true)
		{
			Gems.SetActive(true);
			DeerIcon.SetActive (true);
			BirdIcon.SetActive (true);
			fishIcon.SetActive (true);
			ArrowIcon.SetActive (true);

		}
		if(current!=switchDown)
		{
			if(TimeCounter<=TimeLimit)
			{
				TimeCounter+=Time.deltaTime;
				if(TimeCounter>=TimeLimit/4)
				{
					Gem4.SetActive(false);
				}
				if(TimeCounter>=((TimeLimit/4)*2))
				{
					Gem3.SetActive(false);
				}
				if(TimeCounter>=((TimeLimit/4)*3))
				{
					Gem2.SetActive(false);
				}

			}
			else
			{
				SwitchCharacter(switchDown);
				playerCamera.GetComponent<playerCamScript2> ().distanceUp = CamPositionCharacter[1]; 
				playerCamera.GetComponent<playerCamScript2> ().distanceAway =  CamPositionCharacter[0];
				TimeCounter = 0;
				Gem1.SetActive(false);
				coolDown=true;
			}
		}
		else if(coolDown)
		{
			if(TimeCounter>CoolDownTimer)
			{
				TimeCounter-=Time.deltaTime;
				if(TimeCounter<=CoolDownTimer/4)
				{
					Gem1.SetActive(true);
				}
				if(TimeCounter<=((CoolDownTimer/4)*2))
				{
					Gem2.SetActive(true);
				}
				if(TimeCounter<=((CoolDownTimer/4)*3))
				{
					Gem3.SetActive(true);
				}
			}
			else if(TimeCounter <=CoolDownTimer)
			{
				TimeCounter =0;
				Gem4.SetActive(true);
				coolDown = false;
			}
		}
		else if(TimeCounter >0)
		{
			TimeCounter-=Time.deltaTime;
			if(TimeCounter<=((TimeLimit/4)*3))
			{
				Gem2.SetActive(true);
			}
			if(TimeCounter<=((TimeLimit/4)*2))
			{
				Gem3.SetActive(true);
			}
		}
		else if(TimeCounter<=0)
		{
			TimeCounter = 0;
			Gem4.SetActive(true);
		}
		if (TimeCounter >= 0) {
			if (Input.GetAxis ("DpadVertical") != 0 || Input.GetAxis ("DpadHorizontal") != 0) {
				StartRotation = ArrowIcon.transform.eulerAngles;
				int i = 0;
				foreach (GameObject water in WaterBodies) {
					if (water.GetComponent<Collider> ().bounds.Contains (current.transform.position)) {
						i += 1;
					}
				}
				if (i > 0) {
					inWater = true;
				} else { 
					inWater = false;
				}
				Debug.Log (i);
			}
			if (Input.GetAxis ("DpadVertical") == 1 && UpAvalable && switchUp != current) {
				SwitchCharacter (switchUp);
				CurrentArrowRotation=ArrowUp;
				DeerIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
				BirdIcon.GetComponent<FadeAlpha>().SetAlpha(1f);
				fishIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
				playerCamera.GetComponent<playerCamScript2> ().distanceUp = CamPositionBird[1]; 
				playerCamera.GetComponent<playerCamScript2> ().distanceAway =  CamPositionBird[0]; 
			}
			if (Input.GetAxis ("DpadVertical") == -1 && DownAvalable && switchDown != current) {
				SwitchCharacter (switchDown);
				CurrentArrowRotation=ArrowDown;
				DeerIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
				BirdIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
				fishIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
				playerCamera.GetComponent<playerCamScript2> ().distanceUp = CamPositionCharacter[1];
				playerCamera.GetComponent<playerCamScript2> ().distanceAway = CamPositionCharacter[0];
			}
			if (Input.GetAxis ("DpadHorizontal") == -1 && LeftAvalable && switchLeft != current) {
				SwitchCharacter (switchLeft);
				CurrentArrowRotation=ArrowLeft;
				DeerIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
				BirdIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
				fishIcon.GetComponent<FadeAlpha>().SetAlpha(1f);
				playerCamera.GetComponent<playerCamScript2> ().distanceUp = CamPositionFish[1];
				playerCamera.GetComponent<playerCamScript2> ().distanceAway = CamPositionFish[0];
			}
			if (Input.GetAxis ("DpadHorizontal") == 1 && RightAvalable && switchRight != current) {
				SwitchCharacter (switchRight);
				CurrentArrowRotation=ArrowRight;
				DeerIcon.GetComponent<FadeAlpha>().SetAlpha(1f);
				BirdIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
				fishIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
				playerCamera.GetComponent<playerCamScript2> ().distanceUp = CamPositionDeer[1];
				playerCamera.GetComponent<playerCamScript2> ().distanceAway = CamPositionDeer[0];
			}
		}



	}
	public void SwitchCharacter(GameObject Selected)
	{
		Debug.Log ("SwitchCharacter");
		current.SetActive(false);
		Selected.SetActive(true);
		Selected.transform.position = current.transform.position;
		Selected.transform.eulerAngles = new Vector3 (0,current.transform.eulerAngles.y,0);
		current = Selected;
		current.SetActive(true);
		current.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		current.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		current.SendMessage("EnterWater",inWater);
		playerCamera.GetComponent<playerCamScript2> ().follow = current.transform;
		Audio.Play ();
	}
	public void HitWater()
	{
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
		{	
			enemy.GetComponent<Follower>().speed = 0;
		}
		current.transform.position = GameObject.Find ("SpawnPosition").transform.position;
		SwitchCharacter(switchDown);
		switchDown.transform.GetChild(0).GetComponent<Animator>().SetBool("sitting",true);
		CurrentArrowRotation=ArrowDown;
		DeerIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
		BirdIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
		fishIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
		playerCamera.GetComponent<playerCamScript2> ().distanceUp = CamPositionCharacter[1];
		playerCamera.GetComponent<playerCamScript2> ().distanceAway = CamPositionCharacter [0];
	}
	public void EnterWater()
	{
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
		{	
			enemy.GetComponent<Follower>().speed = 0;
		}
		current.transform.position = GameObject.Find ("SpawnPosition").transform.position;
		switchDown.transform.GetChild(0).GetComponent<Animator>().SetBool("sitting",true);
		CurrentArrowRotation=ArrowDown;
		DeerIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
		BirdIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
		fishIcon.GetComponent<FadeAlpha>().SetAlpha(0.5f);
		playerCamera.GetComponent<playerCamScript2> ().distanceUp = CamPositionCharacter[1];
		playerCamera.GetComponent<playerCamScript2> ().distanceAway = CamPositionCharacter [0];
	}


}
