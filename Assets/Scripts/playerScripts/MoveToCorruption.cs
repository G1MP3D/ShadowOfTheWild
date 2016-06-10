using UnityEngine;
using System.Collections;

public class MoveToCorruption : MonoBehaviour 
{
    Vector3 deathPos;
    Vector3 newPos;
    float yOffset = 55.0f;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(transform.GetComponent<PlayerHealth>().currentHealth <= 0.0f)
        {
            MoveToCorrupt();
            transform.GetComponent<PlayerHealth>().currentHealth = 100.0f;
        }
	}
    public void MoveToCorrupt()
    {
        deathPos = transform.position;
        newPos = new Vector3(deathPos.x, yOffset + 0.1f, deathPos.z);
        transform.position = newPos;
    }
    public void MoveBack()
    {

    }
}
