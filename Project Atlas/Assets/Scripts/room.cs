using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Income
{
    private string key { get; set; }
    private float amount { get; set; }

    public Income(string k, float a)
    {
        key = k;
        amount = a;
    }

}


public class Room : MonoBehaviour {

    //Type (shpuld be turned into an enum)
    private string type { get; set; }

    //Money cost to upkeep
    private float upkeep { set; get; }

    //Income type
    public Income income;

    //size
    private int width { set; get; }
    private int length { set; get; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
