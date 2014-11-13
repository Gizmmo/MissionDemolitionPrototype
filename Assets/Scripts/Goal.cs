using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	//A static field accesable by code anywhere
	static public bool		goalMet = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		//when the trigger is hit by something
		//Check to see if it is a projectile
		if(other.gameObject.tag == "Projectile") {
			//If so set goalMet to true
			Goal.goalMet = true;
			//also se tthe alpha of the color to a higher opacity
			Color c = renderer.material.color;
			c.a = 1;
			renderer.material.color = c;
		}
	}
}
