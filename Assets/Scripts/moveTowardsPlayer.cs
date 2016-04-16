using UnityEngine;
using System.Collections;

public class moveTowardsPlayer : MonoBehaviour {
	public Transform playerTransform;
	public float speed = 5.0f;
	public bool DisableOnContact = false;

	// Use this for initialization
	void Awake()
	{

	
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			//transform.LookAt (playerTransform);
			float distance = speed * Time.deltaTime;
			Vector3 source = transform.position;
			Vector3 target = playerTransform.position;
		transform.position = Vector3.MoveTowards (source, target, distance);	
	}
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag =="Player")
		{
			GameObject.Find("gameScripts").GetComponent<StartGame>().CamOnPlayer = true;
			playerTransform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("StandUp");
			Debug.Log ("hitPlayer");
			if(DisableOnContact)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
