using UnityEngine;
using System.Collections;

public class Spell {

    public string name = "need Name";
    string discription = "need Discription";
    string combo = "";
    float cost = 0;


    public virtual void Update() { }
    public virtual void Cast() { }
   
  
}

public class Invisibility : Spell
{
    // in seconds
    float duration = 5.0f; 
    float castTime = 1.0f;
    float timer = 0; // timer for castTime and duration length

    float AlphaCut = 0;
    bool invisable = false;
    GameObject Target;

   
    enum State
    {
        nullState,
        casting,
        affecting,
        ending,
    }
    State state = State.nullState;

    public override void Update()
    {
        Debug.Log("SPELL UPdate");
        switch(state)
        {
            case State.nullState:
                break;
            case State.casting:
                Debug.Log("SPELL UPdate Cast");
                if (timer < castTime)
                {
                    timer += Time.deltaTime;
                    AlphaCut += (Time.deltaTime / castTime);
                    
                }
                else
                {
                    AlphaCut = 1;
                    invisable = true;
                    state = State.affecting;
                    timer = 0;
                }
                Target.GetComponent<Renderer>().material.SetFloat("_Cutoff", AlphaCut);
                Debug.Log(Target.GetComponent<Renderer>().material.name);
                break;
            case State.affecting:
                Debug.Log("SPELL UPdate affect");
                if (timer<duration)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    state = State.ending;
                    timer = 0;
                }
                break;
            case State.ending:
                if (timer < castTime)
                {
                    timer += Time.deltaTime;
                    AlphaCut -= (Time.deltaTime / castTime);
                }
                else
                {
                    AlphaCut = 0;
                    invisable = false;
                    state = State.nullState;
                    timer = 0;
                }
                Target.GetComponent<Renderer>().material.SetFloat("_Cutoff", AlphaCut);
                Debug.Log(Target.GetComponent<Renderer>().material.GetFloat("_Cutoff"));
                break;
        }
    }
    public override void Cast()
    {
        timer = 0;
        state = State.casting;
        Target = GameObject.Find("testCube");
        Debug.Log("CASTING");
    }

   
}
