using UnityEngine;
using System.Collections;

public class wrap : MonoBehaviour {

	public Transform wrapTarget; //get the position of object 

//void OnTriggerEnter2D(Collider2D other){
	IEnumerator OnTriggerEnter2D(Collider2D other){
		
		if(other.name=="player"){
			ScreenFader sf = GameObject.FindGameObjectWithTag ("Fader").GetComponent<ScreenFader> ();
		yield return StartCoroutine (sf.FadeToBlack());
		other.gameObject.transform.position = wrapTarget.position;
		Camera.main.transform.position= wrapTarget.position;
//Fade in and Fade out animation handler
		yield return StartCoroutine (sf.FadeToClear ());	
	}

}
}