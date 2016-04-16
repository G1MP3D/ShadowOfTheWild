using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControllerBackButton : MonoBehaviour {
	public Button BackButton;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetAxis("BackButton")<0)
		{
			gameObject.GetComponent<Button>().onClick.Invoke();
		}
	}
}
