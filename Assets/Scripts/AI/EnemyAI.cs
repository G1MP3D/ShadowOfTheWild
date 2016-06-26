using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float fieldOfViewAngle = 110.0f;
    public float viewDistance = 10.0f;
    public bool targetInSight =false;
    public Vector3 personalLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    //private LastPlayerSighting lastPlayerSighting;
    private Transform target;
    private float targetDistance;
    //private Animator playerAnim;
    //private PlayerHealth playerHealth;
    //private HashIDs hash;
    private Vector3 previousSighting;

    public enum State
    {
        Spawn,
        despawn,
        die,
        roam,
        chasePlayer
    }
	// Use this for initialization
	void Awake ()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        targetDistance = Vector3.Distance(target.position, transform.position);
        if (targetDistance < viewDistance)
        {
            targetInSight = false;
           // float angle = Vector3.Angle();
           
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    if(hit.collider.gameObject == target)
                    {
                        targetInSight = true;
                    }
                }
            }
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
        }
    }
	void Update () 
    {
	
	}
}
