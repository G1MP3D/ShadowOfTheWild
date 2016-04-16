using UnityEngine;
using System.Collections;

public class Curruptor : MonoBehaviour 
{

		
		public Transform target;
		public float speed;
		public float smooth;
		
		void FixedUpdate () 
		{
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			Vector3 relativePos = target.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation = rotation;
			//transform.rotation = Quaternion.RotateTowards(Quaternion.rotation, Quaterniountarget.rotation,step);
			
			
		}
	}
