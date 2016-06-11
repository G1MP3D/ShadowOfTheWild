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
    public float grvty = 9.81f;
    public bool jumped;
    Vector3 moveDir = Vector3.zero;

    public Animator anim;
	// Use this for initialization
	void Start () 
    {
        anim = transform.GetComponentInChildren<Animator>();
        //anim.SetBool("sitting", true);
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
        //anim.SetBool("sitting", false);
        anim.SetBool("Moving", true);
        Rigidbody.MovePosition(transform.position + (direction * playerWalkSpeed *Time.deltaTime));
        
    }
    public void Jump(Vector3 jumpDir, float jumpHeight)
    {
        if (Rigidbody == null)
        {
            return;
        }
        Rigidbody.velocity += (jumpDir * jumpHeight) + (jumpDist * transform.forward);

        
        Rigidbody.AddForce((jumpDir * jumpHeight));
        jumped = true;
        Debug.Log("Jumped");

    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Ground" && jumped)
        {
            jumped = false;
            
        }
    }
}
