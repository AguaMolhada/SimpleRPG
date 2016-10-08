using UnityEngine;
using System.Collections;

public class Player : PlayerStats {



	// Use this for initialization
	void Start () {
        base.Start();
        Debug.Log(hp);
        TakeDamage(10);
        Debug.Log(hp);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
