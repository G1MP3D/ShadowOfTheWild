using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
    float playerHealth;
    public float currentHealth;
	// Use this for initialization
	void Start () 
    {
        playerHealth = 100.0f;
        currentHealth = playerHealth;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(currentHealth == 0.0f)
        {
           
        }
	}
    public void Damage(float dmg)
    {
        Debug.Log("Damage Taken");

        currentHealth = currentHealth - dmg;
    }
}
