using UnityEngine;
using System.Collections;

public class PracticeMovmnt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public float movementSpeed = 10;
	public float turningSpeed = 60;
	// Update is called once per frame
		
	void Update() {
		float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
		transform.Rotate(0, horizontal, 0);

		float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
		transform.Translate(0, 0, vertical);
	}
}


