using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class MoveAnimation : MonoBehaviour {

	Rigidbody2D rbody;
	Animator anim;

	public float speed;
	public bool controls;
	string touching;

	public InputField name;
	public RPGTalk rpgTalk;
	public GameObject askwho;
	public RPGTalk rpgTalkToFollow;



	public GameObject wall;
	public GameObject particle;

    [HideInInspector]
    public bool BoostCheck;
    public bool TalkCheck;


	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	
		
	
		//skip the Talk to the end
		if(Input.GetKeyDown(KeyCode.Return)){
			rpgTalk.EndTalk ();
		}
		if (controls) {
			//Vector2 movement_vector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
			Vector2 movement_vector = new Vector2 (CrossPlatformInputManager.GetAxis("Horizontal"),CrossPlatformInputManager.GetAxis("Vertical"));
			if (movement_vector != Vector2.zero) {
				anim.SetBool ("iswalking", true);
				anim.SetFloat ("inputx", movement_vector.x); 
				anim.SetFloat ("inputy", movement_vector.y);
				
			} else {
				anim.SetBool ("iswalking", false);
			}
	
			if (Input.GetKey (KeyCode.LeftShift )|| BoostCheck){
				speed = 2;
				anim.speed = 2;
			} else {
				speed = 1;
				anim.speed = 1;
			}

            Debug.Log(speed+" Speedde  "+anim.speed);
			



			rbody.MovePosition (rbody.position + movement_vector * speed * Time.deltaTime);
		

			if (Input.GetKeyDown (KeyCode.E) || TalkCheck ) {
				if (touching == "FunnyGuy") {
					controls = false;
					rpgTalk.lineToStart = 49;
					rpgTalk.lineToBreak = 53;
					rpgTalk.callbackFunction = "WhoAreYou";
					rpgTalk.NewTalk ();
				}
				if (touching == "Molly") {
					controls = false;
					rpgTalk.lineToStart = 36;
					rpgTalk.lineToBreak = 39;
					rpgTalk.callbackFunction = "ByeWall";
					rpgTalk.callbackFunction = "GiveBackControls";
					rpgTalk.shouldStayOnScreen = false;
					rpgTalk.NewTalk ();
				}


			}
		} else {
			anim.speed = 0;
		}

	}


	//Android Button
	public void Boost(){
	
		speed = 4;
		anim.speed = 2;

        Debug.Log(speed + " SpeeddeUPPP  " + anim.speed);
    }
	public void BoostOver(){

		speed = 1;
		anim.speed = 1;
        Debug.Log(speed + " SpeeddeOVVVErr  " + anim.speed);
    }







	//give the controls to player
	public void GiveBackControls(){
		controls = true;
	}

	//Open the screen to enter Player's name
	public void WhoAreYou(){
			

			//controls = true;
	        askwho.SetActive(true);
	 		name.Select ();
		}
	public void IKnowYouNow(){
		askwho.SetActive (false);
	    rpgTalk.variables [0].variableValue = name.text;
		rpgTalk.lineToStart = 55;
		rpgTalk.lineToBreak = 58;
	//	rpgTalk.callbackFunction = "ByeWall";
		rpgTalk.callbackFunction = "GiveBackControls";
		rpgTalk.NewTalk ();
	}
	public void ByeWall(){
	    wall.SetActive (false);
		particle.SetActive (true);
	//	Invoke ("GirlEnd", 2f);
	}


	void GirlEnd(){
		rpgTalk.lineToStart = 41;
		rpgTalk.lineToBreak = 43;
		rpgTalk.callbackFunction = "GiveBackControls";
		rpgTalk.NewTalk ();
	}
	void OnTriggerEnter2D(Collider2D col){
		touching = col.name;
		if(touching == "AlgaRafi"){
			rpgTalkToFollow.NewTalk ();
		}
		if (touching == "PGameLevel1" && GameManager.Level==0) {
			Application.LoadLevel("game");
		}
	}

	void OnTriggerExit2D(Collider2D col){
		touching = "";
		Invoke ("EndTalk", 5f);

	}
	void EndTalk(){
		rpgTalkToFollow.EndTalk ();
	
	}

}
