using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	static public FollowCam 	S; //a FollowCam Singleton
	public float				easing = 0.05f;
	public Vector2				minXY;

	private GameObject			_poi;//The point of intrest
	private float				camZ; //the desired Z pos of the camera

	//Returns the location of the most recently added point
	public GameObject poi {
		get {
			return _poi;
		}
		set {
			_poi = value;
		}
	}


	// Use this for initialization
	void Start () {
		S = this;
		camZ = this.transform.position.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 destination;
		//If there is no poi, return to P:[0,0,0]
		if (_poi == null){
			destination = Vector3.zero;
		} else{
			
			//Get the position of the poi
			destination = _poi.transform.position;
			
			//If poi is a Projectile, check to see if it's at rest
			if (_poi.tag == "Projectile"){
				//if it is sleeping (that is, not moving)
				if(_poi.rigidbody.IsSleeping()){
					//return to default view
					_poi = null;
					//in the next update
					return;
				}
			}
		}

		// Limit the X & Y to minimum values
		destination.x = Mathf.Max(minXY.x, destination.x);
		destination.y = Mathf.Max(minXY.y, destination.y);

		//Interpolate from the current camera position towards destination
		destination = Vector3.Lerp (transform.position, destination, easing);
		//Retain a destination.z of camZ
		destination.z = camZ;
		//Set the camera to the destination
		transform.position = destination;
		//Set the orthographicSize of the camera to keep ground in view
		this.camera.orthographicSize = destination.y + 10;
	}
}
