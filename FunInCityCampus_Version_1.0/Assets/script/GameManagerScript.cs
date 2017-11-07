using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	// Use this for initialization
	public int myScore;
	public Text myscoreGUI;
	void Start () {
		myScore = 0;
		myscoreGUI = GameObject.Find ("Player Score").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void GmAddScore(){
		this.myScore = this.myScore + 5;
		myscoreGUI.text = myScore.ToString ();
	}
}
