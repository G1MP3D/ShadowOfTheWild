using UnityEngine;
using System.Collections;

public class PlayerMovement_Motor : MonoBehaviour 
{
    Rigidbody _rigidbody;
    private Rigidbody Rigidbody
    {
        get
        {
            if(_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }
            return _rigidbody;
        }
    }

    public float playerWalkSpeed = 5.0f;
    public float jumpheight = 10.0f;
    public float jumpDist = 3.0f;
    public float grvty;
    public bool jumped;
    Vector3 moveDir = Vector3.zero;

    Animator anim;
    public Animator anim;
	// Use this for initialization
	void Start () 
    {
        anim = transform.FindChild("character").GetComponent<Animator>();
        anim.SetBool("sitting", true);
        jumped = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
    public void Move(Vector3 direction, float walkSpeed)
    {
        if (Rigidbody == null)
        {
            return;
        }
        anim.SetBool("sitting", false);
        anim.SetBool("isWalking", true);
        //anim.SetBool("sitting", false);
        //anim.SetBool("isWalking", true);
        Rigidbody.AddForce(direction * walkSpeed);
        
    }
    public void Jump(Vector3 jumpDir, float jumpHeight)
    {
        if(Rigidbody == null)
        {
            return;
        }
        Rigidbody.velocity += (jumpDir *jumpheight) + (jumpDist * transform.forward);

       
        Rigidbody.AddForce(jumpDir * jumpHeight);
        jumped = true;
        Debug.Log("Jumped");
        
    }
    public void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Ground" && jumped )
        {
            jumped = false;
            Debug.Log(jumped);
        }
    }
}
