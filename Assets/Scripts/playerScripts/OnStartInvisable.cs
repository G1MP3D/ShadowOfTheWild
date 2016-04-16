using UnityEngine;
using System.Collections;

public class OnStartInvisable : MonoBehaviour {
	public bool invisable;
	// Use this for initialization
	void Start () {
		if(invisable)
		{
			gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
