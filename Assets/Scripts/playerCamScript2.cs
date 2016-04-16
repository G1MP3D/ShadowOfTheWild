using UnityEngine;
using System.Collections;

public class playerCamScript2 : MonoBehaviour {
	public Transform follow;
	Vector3 targetPosition;
	public float distanceAway;
	public float distanceUp;
	public float lookSpeed;
	public float smooth;
	Vector3 velocityCamSmooth = Vector3.zero;
	float camSmoothDampTime = 0.5f;

	Vector3 LookDirection;
	public Vector3 offset = new Vector3(0,1,0);
	float MouseXInput;
	float MouseYInput;
	//Vector3 camShift = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//MouseXInput = Input.GetAxis ("Mouse X");
		//MouseYInput = Input.GetAxis ("Mouse Y");


	
	}
	void LateUpdate()
	{

		Vector3 characterOffset = follow.position + offset;
		LookDirection = characterOffset - this.transform.position;
		LookDirection.y = 0.0f;
		LookDirection.Normalize ();
		//Debug.DrawRay (transform.position, LookDirection, Color.green);
		targetPosition = characterOffset +(follow.up * distanceUp) - (LookDirection * distanceAway);

		SmoothPosition(transform.position, targetPosition);

		//camShift +=  new Vector3(-MouseXInput * smooth * Time.deltaTime, MouseYInput *smooth* Time.deltaTime,0 );
		transform.LookAt(follow.position);



	}
	void SmoothPosition(Vector3 fromPosition,Vector3 ToPosition)
	{
		this.transform.position = Vector3.SmoothDamp (fromPosition, ToPosition, ref velocityCamSmooth, camSmoothDampTime);
	}
}
