using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class base_building : MonoBehaviour {

    public int maxHealth { get; set; }
    public int health{ get; set; }
    public string name { get; set; }
    public float foodDemand { get; set; }
    public float energyDemand { get; set; }
    public Dictionary<string, int> loyalty { get; set; }
    public string currentOwner = "id_neutral";
    public bool isDestroyed = false;

      

// Use this for initialization
void Start () {
        loyalty = new Dictionary<string, int>();
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loyaltyInit()
    {
        loyalty.Add("neutral", 100);
    }

    public bool checkOwnership(string owner)
    {
        string curMax = "id_neutral";
        int curMaxShares=0;
        foreach(KeyValuePair<string,int> councilMember in loyalty)
        {
            if(councilMember.Value > curMaxShares)
            {
                curMax = councilMember.Key;
                curMaxShares = councilMember.Value;
            }
        }
        return owner == curMax;
    }

    public bool buyShares(int shares, string player)
    {
        
        if(loyalty["id_neutral"] >= shares)
        {
            loyalty["id_neutral"] -= shares;
            if (loyalty.ContainsKey(player))
            {
                loyalty[player] += shares;
            }else
            {
                loyalty.Add(player, shares);
            }
            return true;
        }
        return false;
        
    }

    public int getShares(string id)
    {
        if (loyalty.ContainsKey(id))
        {
            return loyalty[id];
        }
        return 0;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            isDestroyed = true;
            //Call destroy function
        }
    }

    public void heal(int damageHealed)
    {
        health += damageHealed;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    
}
