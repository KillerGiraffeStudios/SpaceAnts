using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global_ship : MonoBehaviour {

    // The ship's current health
    private int health;

    // The current happiness level of the population
    private int happiness;

    // Current food in the ship
    private int totalFood;

    // The total population of the ship
    private int totalPopulation;

    // Array of the rooms contained in the ship
    private room[,] rooms;

	// Use this for initialization
	void Start () {
        this.health = 100;
        this.happiness = 50;
        this.totalFood = 200;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
    {
        return this.health;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetHappiness()
    {
        return this.happiness;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetTotalFood()
    {
        return this.totalFood;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetTotalPopulation()
    {
        return this.totalPopulation;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        this.health = this.health - damage;
        // Check the integrety of the ship and end game if it has reached 0
        if(this.health <= 0)
        {
            // Call function to end game
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="change"></param>
    public void ChangeHappiness(int change)
    {
        this.happiness = this.happiness + change;
        if(this.happiness <= 0)
        {
            // Call revolution function
        }
    }
}
