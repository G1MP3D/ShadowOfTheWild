using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpells : MonoBehaviour {
    public List<Spell> playerSpells = new List<Spell>();
    public bool CastINVis = false;
	// Use this for initialization
	void Start () {

        playerSpells.Add(new Invisibility());

	}
	
	// Update is called once per frame
	void Update () {
	    if(CastINVis)
        {
            playerSpells[0].Cast();
            CastINVis = false;
        }
        for(int i =0; i<playerSpells.Count;i++)
        {
            playerSpells[i].Update();
        }
	}
}
