using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {
	// player position 
	public Transform target;
	Camera mycam;
	public float movespeed=0.1f;
	// Use this for initialization
	void Start () {
		mycam = GetComponent<Camera> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {
			//mycam.orthographicSize = (Screen.height / 100f) / 4f;
			transform.position = Vector3.Lerp (transform.position, target.position, movespeed) + new Vector3 (0, 0, -10);
		}
	
	}
}
