using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	//fields set in the Unity Inspector Pane
	public GameObject		launchPoint;
	public GameObject		prefabProjectile;
	public float			velocityMult = 4f;

	//private fields
	private Vector3			launchPos;
	private GameObject		projectile;
	private bool			aimingMode;

	void Awake () {
		launchPoint.SetActive(false);
		launchPos = launchPoint.transform.position;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDrag();
	}

	void OnMouseEnter() {
//		print ("Slingshot:OnMouseEnter");
		launchPoint.SetActive(true);
	}

	void OnMouseExit() {
//		print ("Slingshot:OnMouseExit");
		launchPoint.SetActive(false);
	}

	void OnMouseDown () {
		//the player has pressed the mouse button while over the slingshot
		aimingMode = true;
		//Instantiate a Projectile
		projectile = Instantiate(prefabProjectile) as GameObject;
		//Start at the launchpoint
		projectile.transform.position = launchPos;
		//Set it to isKinematic for now
		projectile.rigidbody.isKinematic = true;
	}

	void UpdateDrag() {
		//If Slignshot is not in aiming mode, do not continue
		if(!aimingMode) return;

		//Get the current mouse position in 2D screen coords
		Vector3 mousePos2D = Input.mousePosition;

		//Convert the mouse position to 3D world coords
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

		//Find the delta from the lauchPos to the mousePos3D
		Vector3 mouseDelta = mousePos3D - launchPos;

		//Limit mouseDelta to the radius of the slingshot SphereCollider
		float maxMagnitude = this.GetComponent<SphereCollider>().radius;
		if(mouseDelta.magnitude > maxMagnitude) {
			mouseDelta.Normalize();
			mouseDelta *= maxMagnitude;
		}

		//Move the Projectile to this new position
		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;

		if(Input.GetMouseButtonUp(0)) {
			//The mouse has been released
			aimingMode = false;
			projectile.rigidbody.isKinematic = false;
			projectile.rigidbody.velocity = -mouseDelta * velocityMult;
			projectile = null;
		}
	}
}
