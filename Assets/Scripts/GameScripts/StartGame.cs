using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	public string CameraName;

	public float fadeTime;
	public int offset;
	public float ControllerTimer;
	public bool CamOnPlayer = false;
	GameObject PlayerCamera;
	public GameObject[] TitleScreenObjects;
	GameObject Player;
	Transform startCamTarget;

	Transform playerCamTarget;
	bool fade = false;
	float fadeTimer =0;
	float controlTimer = 0;


	void Awake()
	{

		PlayerCamera = GameObject.Find (CameraName);
		startCamTarget = PlayerCamera.GetComponent<playerCamScript2> ().follow;
		Player = GameObject.Find ("Character");

		if(GameObject.FindGameObjectWithTag ("PlayerCamTarget") == null)
		{
			playerCamTarget = Player.transform;	
		}
		else
		{
			playerCamTarget = GameObject.FindGameObjectWithTag ("PlayerCamTarget").transform;
		}
	}
	// Use this for initialization
	void Start () 
	{
		Player.GetComponent<basicMovment>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if ( CamOnPlayer && controlTimer <= ControllerTimer) 
		{
			controlTimer += Time.deltaTime;
		}
		else if(controlTimer > ControllerTimer)
		{
			Player.GetComponent<basicMovment>().enabled = true;
			PlayerCamera.GetComponent<playerCamScript2>().follow = playerCamTarget;
		}
		if(TitleScreenObjects[0].activeInHierarchy == true && Input.GetButtonDown("StartButton"))
		{
			foreach(GameObject titleObjects in TitleScreenObjects)
			{
				titleObjects.SendMessage("StartFade",new Vector2(fadeTime,-1));

			}
			fade=true;
		}
		else if(TitleScreenObjects[0].activeInHierarchy == false && PlayerCamera.GetComponent<playerCamScript2>().follow == playerCamTarget)
		{
			this.enabled =false;
		}
		if(fade && fadeTimer <= fadeTime + offset)
		{
			fadeTimer +=Time.deltaTime;
		}
		else if( fadeTimer>fadeTime + offset)
		{
			startCamTarget.GetComponent<Animator>().enabled = false;
			startCamTarget.GetComponent<moveTowardsPlayer>().enabled = true;
		}

	}

}
