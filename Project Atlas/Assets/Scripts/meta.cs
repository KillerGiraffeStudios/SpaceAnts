using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	static GameObject META;

	/// <summary>
	/// Returns the object with the Metascript attached to it
	/// </summary>
	/// <returns></returns>
	private static GameObject GetMeta(){
		if(META == null)
			META = GameObject.Find("Meta");
		return META;
	}

	// Example for getting a component
	static Meta METASCRIPT;
	public static Meta GetMetaScript(){
		if(META == null)
			METASCRIPT = GetMeta().GetComponent<Meta>();
		return METASCRIPT;
	}
}
