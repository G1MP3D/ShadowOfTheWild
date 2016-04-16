using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	GameObject PlayerCamera;
	// Use this for initialization
	void Start () {
		PlayerCamera = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(PlayerCamera.transform);
	}
}
