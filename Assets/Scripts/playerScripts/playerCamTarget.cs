using UnityEngine;
using System.Collections;

public class playerCamTarget : MonoBehaviour {
	GameObject PlayerCharacters;
	// Use this for initialization
	void Awake () {

		transform.SetParent (PlayerCharacters.GetComponent<Switch> ().switchDown.transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
