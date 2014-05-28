using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	// Use this for initialization
	public int speed =  5;
	public double dodgeTime = 2.0;
	public double attackTime = 0.2;
	public int attackSpeed = 40;
	public int dodgeSpeed = 20;
	public int sprintSpeed = 10;
	public int walkSpeed = 5;
	public double startDodge;
	public double startAttack;
	public bool dodged = false;
	public bool attacking = false;
	public bool movingForward = false;
	public bool debugging = false;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton ("Horizontal")) {

			if(Input.GetAxis("Horizontal") < 0 ){
				transform.Translate (new Vector3(1,0,0) * Time.deltaTime * speed);
			}
			else{
			transform.Translate (new Vector3(-1,0,0) * Time.deltaTime*speed);
			}
		}
		if(Input.GetButton ("Vertical")){
			// backwards
			if(Input.GetAxis("Vertical")<0){
				transform.Translate (new Vector3(0,0,-1) * Time.deltaTime*speed);
				movingForward = false;
			}
			// forwards
			else{
				transform.Translate (new Vector3(0,0,1) * Time.deltaTime*speed);
				movingForward = true;
			}
		}
		if(Input.GetButtonDown("Jump")){

			transform.Translate (new Vector3(0,0,10) * Time.deltaTime*speed);
		}
		if(Input.GetButton ("Sprint")){
			speed = sprintSpeed;
			if(dodged){
				speed = dodgeSpeed;
			}
		}
		if(Input.GetButtonUp ("Sprint")){
			speed = walkSpeed;
		}
		if (debugging) {
			Debug.Log (speed);
		}
	
		if(Input.GetButtonDown ("Dodge") && !dodged){
			//dodgeTime = 0.5; // may be problem
			startDodge = Time.time;
			speed = dodgeSpeed;
			dodged = true;
		}
		if (debugging) {
			Debug.Log (startDodge);
			double endDodge = startDodge + dodgeTime;
			Debug.Log ("Projected stop: " + endDodge);
			Debug.Log ("Dodgetime: " + dodgeTime);
		}
		if (Time.time > startDodge + dodgeTime && dodged) {
			Debug.Log ("Finish time: " + Time.time); 
			speed = walkSpeed;
			dodged = false;
		}
		if (Input.GetButtonDown ("Attack") && !attacking) {
			startAttack = Time.time;

			if(movingForward)
			{
			speed = attackSpeed;
			}

			attacking= true;
		}
		if (Time.time > startAttack + attackTime && attacking) {
			speed = walkSpeed;
			attacking = false;

		}

	}
}
