using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	public int playerNumber =0;
	public float speed =0f;

	// Use this for initialization
	void Start () {
		speed = 0;
	}
	
	// Update is called once per frame
	void Update () {

		movef ();
		
	}

	public void movef(){

		transform.position += Vector3.right * Time.deltaTime * speed;

			speed -= 0.1f;

		if (speed < 0) {
			speed = 0;
		}

		if (speed > 10) {
			speed = 10;
		}
		

	}

	public void Speed(int x){
		if (x == playerNumber) {
			speed++;
		}
	}
}
