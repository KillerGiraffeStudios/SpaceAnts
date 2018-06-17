using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global_ship : MonoBehaviour {

    // The ship's current health
    private int health;

    // The current happiness level of the population
    private int happiness;

    // Array of the rooms contained in the ship
    //private room rooms[][];

	// Use this for initialization
	void Start () {
        this.health = 100;
        this.happiness = 50;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
