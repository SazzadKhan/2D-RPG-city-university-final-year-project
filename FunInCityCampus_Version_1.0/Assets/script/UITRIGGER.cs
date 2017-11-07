using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITRIGGER : MonoBehaviour {

	// Use this for initialization
	public Canvas myCanvas;
	void Start () {
		
		
	}
	
	// Update is called once per frame
	/*void Update () {
		if (Input.GetKey (KeyCode.J)) {
			myCanvas.enabled = true;
		} else {
			myCanvas.enabled = false;
		}
	}*/
		

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag== "rafi")
		myCanvas.enabled = false;
	}

	//If you want to be more specific to what gets enabled and store it all in one script you can check tags

	void OnTriggerEnter2D(Collider2D other){
		
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "rafi") {
			myCanvas.enabled = true;

		} else{
			myCanvas.enabled = false;
		}
	

	}





}
