using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class finish : MonoBehaviour {

	bool win = false;

	// Use this for initialization
	void Start () {

		GameObject.Find ("win text").GetComponent<Text>().text = "";

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Player" && !win) {
			int winner = col.gameObject.GetComponent<move> ().playerNumber;	
			GameObject.Find ("win text").GetComponent<Text>().text = "Player " + winner + " wins" ;
			win = true;

		}
	}
}
