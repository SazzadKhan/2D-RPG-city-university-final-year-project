using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	Animator anim;
	bool isFading= false;// veriables for animator

	void Start () {
		anim = GetComponent<Animator> ();
	
	}
	public IEnumerator FadeToClear(){
	
		isFading = true;
		anim.SetTrigger ("FadeIn");
		while (isFading) 
			yield return null; // time to perform fade in
	}
	public IEnumerator FadeToBlack(){

		isFading = true;
		anim.SetTrigger ("FadeOut");
		while (isFading) {
			yield return null; // wait for the given time 
		}
	}
	void AnimationComplete(){
		isFading = false;
	}
}
	

