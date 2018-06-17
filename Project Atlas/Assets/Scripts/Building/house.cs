using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house : base_building {
    
	// Use this for initialization
	void Start () {
        maxHealth = 10;
        health = 10;
        energyDemand = 1;
        foodDemand = 2;
        name = "house";

        loyaltyInit();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
