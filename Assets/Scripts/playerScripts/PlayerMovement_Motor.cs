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

    bool hitFound;

    int groundLayer = (1 << 10);

    Ray ray;
    RaycastHit hit;

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
        if(jumped)
        {
            hitFound = Physics.Raycast(transform.position, Vector3.down, out hit, 5.0f, groundLayer);
            
            if(hitFound)
            {
                if(Vector3.Distance(transform.position,hit.point) <= 0.5f)
                {
                    jumped = false;
                }
            }
        }
	}
    public void Move(Vector3 direction, float walkSpeed)
    {
        if (Rigidbody == null)
        {
            return;
        }
        //anim.SetBool("sitting", false);
        //anim.SetBool("Moving", true);
        Rigidbody.MovePosition(transform.position+ (direction * playerWalkSpeed * Time.deltaTime));
        
    }
    public void Jump(Vector3 jumpDir, float jumpHeight, Vector3 direction)
    {
        if (Rigidbody == null)
        {
            return;
        }
        Rigidbody.velocity += ((jumpDir * jumpHeight) + (direction))/2;

        
        Rigidbody.AddForce((jumpDir * jumpHeight));
        jumped = true;
        Debug.Log("Jumped");
    }

}
