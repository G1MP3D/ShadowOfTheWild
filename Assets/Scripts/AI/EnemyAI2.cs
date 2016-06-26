using UnityEngine;
using System.Collections;

public class EnemyAI2 : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform target;
    private fieldOfView fov;
    public float followViewDist = 40;
    public float followViewAngle = 360;
    public float defaultViewDist = 15;
    public float defaultViewAngle = 110;
    bool waiting = false;
    public float waitTime = 2.0f;
    public float waitTimer = 0.0f;
    public enum State
    {
        Roam,
        Patrol,
        Wait,
        Follow,
        LastKnow
    }
    public State state = State.Roam;
    State ReturnState = State.Roam;
	void Awake ()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(transform.position);
        fov = GetComponent<fieldOfView>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(agent.destination);
        switch(state)
        {
            case State.Roam:
                Roaming();
                if(fov.visibleTargets.Count>0)
                {
                    ChangeViewAngleAndDistance(followViewAngle, followViewDist);
                    SetTarget(fov.visibleTargets[0]);
                    ReturnState = state;
                    state = State.Follow;
                }
                break;
            case State.Follow:
                if (fov.visibleTargets.Count > 0)
                {
                    agent.SetDestination(target.position);
                }
                else
                {
                    state = State.LastKnow;
                    ChangeViewAngleAndDistance(180, followViewDist);
                }
                break;
            case State.LastKnow:
               
                if (agent.remainingDistance < 0.01f)
                {
                    state = State.Wait;
                    waitTimer = 0;
                }
                    break;
            case State.Wait:
                waitTimer += Time.deltaTime;
                if(waitTimer >= waitTime)
                {
                    state = ReturnState;
                    ChangeViewAngleAndDistance(defaultViewAngle, defaultViewDist);
                }
                break;

        }
        //agent.SetDestination(target.position);
	}
    void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    void Roaming()
    {
        //check to see if destination has been reached
        if (agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <0.01f)
        {
            Vector3 destination;
            float ChanceOfWait =  Random.Range(0, 10);
            if(ChanceOfWait<=5 && waiting == false)
            {
                waitTimer = 0;
                waiting = true;
                state = State.Wait;
                ReturnState = State.Roam;
            }
            else if (RoamWayPoint(transform.position, 10, 50, out destination))
            {
                waiting = false;
                agent.SetDestination(destination);
            }
        }
       
    }
    //picks a random position in range from a set point and returns it if it is on the nav mesh
    bool RoamWayPoint (Vector3 center, float MinRange,float MaxRange, out Vector3 result)
    {
        float range = Random.Range(MinRange, MaxRange);
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                Debug.Log("TRUE");
                return true;
                
            }
        }
            result = Vector3.zero;
            return false;
    }
    void ChangeViewAngleAndDistance(float angle, float distance)
    {
        fov.viewAngle = angle;
        fov.ViewRadius = distance;
    }
}
