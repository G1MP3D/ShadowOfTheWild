using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour 
{
    float timedDamage;
    bool trigEntered;
    bool timeZero;
    public Transform collidedObject;
    float dmg =25.0f;
	// Use this for initialization
	void Start () 
    {
        trigEntered = false;
        timeZero = false;
        timedDamage = 5.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(trigEntered)
        {
            timedDamage -= Time.deltaTime;
            Debug.Log(timedDamage);
            if(timedDamage <= 0.0f)
            {
                Debug.Log("AboutToBeDamaged");
                collidedObject.SendMessage("Damage",dmg,SendMessageOptions.DontRequireReceiver);
                Debug.Log("DamageCalled");
                timedDamage = 5.0f;
            }
        }
	}
    public void OnTriggerStay(Collider col)
    {
        if(col.transform.tag == "Player")
        {
            trigEntered = true;
            Debug.Log(trigEntered);
            collidedObject = col.transform.root;
        }
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            trigEntered = false;
            Debug.Log(trigEntered);
            timedDamage = 5.0f;
            collidedObject = null;
        }
    }
}
