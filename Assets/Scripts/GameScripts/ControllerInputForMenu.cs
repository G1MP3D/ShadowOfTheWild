using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ControllerInputForMenu : MonoBehaviour {
	public GameObject Menu;
	public float analogStickTimer =1;
	float TimeStamp =0;
	Button[] buttonArray;
	public int currentButton =1;
	int dpadDirrectionY = 0;
	int leftStickDirectionY=0;
	public GameObject Title;
	public GameObject Pause;

	// Use this for initialization
	void Start () {
		buttonArray = new Button[ Menu.transform.childCount];
		for(int i=0;i<buttonArray.Length;i++)
		{
			buttonArray[i]=Menu.transform.GetChild(i).GetComponent<Button>();
		}
		Pause.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("DpadInput: " + Input.GetAxis ("menuDPad") + " LeftStick: " + Input.GetAxis ("VerticalPlayerOne"));
		if(Input.GetButtonDown("StartButton"))
		{
			if(Title.activeInHierarchy ==false)
			{
				Pause.GetComponent<PauseGame>().Paused = true;
				Pause.SetActive(true);

			}
		}
		if(Input.GetButtonDown("Cancel"))
		{
			Application.Quit();
		}

		if((int)Input.GetAxis("DpadVertical")<0)
		{
			if(Time.time-TimeStamp >=analogStickTimer || dpadDirrectionY!=-1)
			{
				TimeStamp=Time.time;
				if(currentButton >=buttonArray.Length-1)
				{
					currentButton=0;
				}
				else
				{
					currentButton+=1;
				}
				dpadDirrectionY=-1;
			}
			
		}
		else if((int)Input.GetAxis("DpadVertical")>0)
		{
			if(Time.time-TimeStamp >=analogStickTimer || dpadDirrectionY!=1)
			{
				TimeStamp=Time.time;
				if(currentButton <=0)
				{
					currentButton = buttonArray.Length-1;
				}
				else
				{
					currentButton-=1;
				}
				dpadDirrectionY=1;
			}
			
		}
		dpadDirrectionY = (int)Input.GetAxis ("DpadVertical");

		if((int)Input.GetAxis("JoyStickVertical")<0)
		{
			if(Time.time-TimeStamp >=analogStickTimer || leftStickDirectionY!=-1)
			{
				TimeStamp=Time.time;
				if(currentButton >=buttonArray.Length-1)
				{
					currentButton=0;
				}
				else
				{
					currentButton+=1;
				}
				leftStickDirectionY=-1;
			}

		}
		else if((int)Input.GetAxis("JoyStickVertical")>0)
		{
			if(Time.time-TimeStamp >=analogStickTimer || leftStickDirectionY!=1)
			{
				TimeStamp=Time.time;
				if(currentButton <=0)
				{
					currentButton = buttonArray.Length-1;
				}
				else
				{
					currentButton-=1;
				}
				leftStickDirectionY=1;
			}
			
		}
		leftStickDirectionY = (int)Input.GetAxis ("JoyStickVertical");
		buttonArray[currentButton].Select();
		if(Menu.activeInHierarchy)
		{
			if(Input.GetAxis("Jump")>0)
			{
				buttonArray[currentButton].SendMessage("OnClick");
			}
		}
		 

	}
	public void Restart()
	{
		Application.LoadLevel (0);
	}
	public void Continue()
	{
		Pause.GetComponent<PauseGame>().Paused = false;
		Pause.SetActive(false);
	}
	public void Exit()
	{
		Application.Quit();
	}
}
