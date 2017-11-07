using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
		// declear veriable type to put game componet
	GameManagerScript gameManager;
	private AudioSource audioSource;
		
		// Use this for initialization

	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManagerScript> ();
		audioSource = gameObject.GetComponent<AudioSource> ();
	}
		// On trigger functionalities with Player collision
		
	void OnTriggerEnter2D(Collider2D other)
	{if (other.gameObject.name == "player") { 
		
		// add score play sound and self destroy after .5 seconds
			gameManager.GmAddScore();
			audioSource.Play ();

			Invoke("Destroy",.5f);
		}
	}
	void Destroy(){
		Destroy(gameObject);
	}
}
