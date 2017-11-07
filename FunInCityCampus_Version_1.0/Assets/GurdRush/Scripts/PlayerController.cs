using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{

    public float speed = 0.4f;
    Vector2 _dest = Vector2.zero;
    Vector2 _dir = Vector2.zero;
    Vector2 _nextDir = Vector2.zero;

    [Serializable]
    public class PointSprites
    {
        public GameObject[] pointSprites;
    }

    public PointSprites points;

    public static int killstreak = 0;

    // script handles
    private GameGUINavigation GUINav;
    private GameManager GM;
    private ScoreManager SM;


    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;

    private bool _deadPlaying = false;

    // Use this for initialization
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        SM = GameObject.Find("Game Manager").GetComponent<ScoreManager>();
        GUINav = GameObject.Find("UI Manager").GetComponent<GameGUINavigation>();
        _dest = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (GameManager.gameState)
        {
            case GameManager.GameState.Game:
                ReadInputAndMove();
                Animate();
                break;

		case GameManager.GameState.Dead:
			if (!_deadPlaying) {
				StartCoroutine ("PlayDeadAnimation");

			}
                break;
        }


    }

    IEnumerator PlayDeadAnimation()
    {
        _deadPlaying = true;
        GetComponent<Animator>().SetBool("Die", true);
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("Die", false);
        _deadPlaying = false;

        if (GameManager.lives <= 0)
        {
         
         
            GUINav.H_ShowGameOverScreen();
			Application.LoadLevel ("prototyping");
			
        }

        else
            GM.ResetScene();
    }

    void Animate()
    {
        Vector2 dir = _dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool Valid(Vector2 direction)
    {
        // cast line from 'next to pacman' to pacman
        // not from directly the center of next tile but just a little further from center of next tile
        Vector2 pos = transform.position;
        direction += new Vector2(direction.x * 0.45f, direction.y * 0.45f);
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return hit.collider.name == "pacdot" || (hit.collider == GetComponent<Collider2D>());
    }

    public void ResetDestination()
    {
        _dest = new Vector2(15f, 11f);
        GetComponent<Animator>().SetFloat("DirX", 1);
        GetComponent<Animator>().SetFloat("DirY", 0);
    }

    void ReadInputAndMove()
    {
        // move closer to destination
        Vector2 p = Vector2.MoveTowards(transform.position, _dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        // get the next direction from keyboard
		if (CrossPlatformInputManager.GetAxis("Horizontal") > 0) _nextDir = Vector2.right;
		if (CrossPlatformInputManager.GetAxis("Horizontal") < 0) _nextDir = -Vector2.right;
		if (CrossPlatformInputManager.GetAxis("Vertical") > 0) _nextDir = Vector2.up;
		if (CrossPlatformInputManager.GetAxis("Vertical") < 0) _nextDir = -Vector2.up;



       


            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            
                            isSwipe = true;
                            fingerStartTime = Time.time;
                            fingerStartPos = touch.position;
                            break;

                        case TouchPhase.Canceled:
                                 // The touch is being canceled 
                            isSwipe = false;
                            break;

                        case TouchPhase.Ended:
                            float gestureTime = Time.time - fingerStartTime;
                            float gestureDist = (touch.position - fingerStartPos).magnitude;

                            if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                            {
                                Vector2 direction = touch.position - fingerStartPos;
                                Vector2 swipeType = Vector2.zero;

                                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                                {
                                    // the swipe is horizontal:
                                    swipeType = Vector2.right * Mathf.Sign(direction.x);
                                }
                                else
                                {
                                    // the swipe is vertical:
                                    swipeType = Vector2.up * Mathf.Sign(direction.y);
                                }
                                if (swipeType.x != 0.0f)
                                {
                                    if (swipeType.x > 0.0f)
                                    {
                                    // MOVE RIGHT
                                    _nextDir = Vector2.right;

                                }
                                    else
                                    {
                                    // MOVE LEFT
                                    _nextDir = - Vector2.right;
                                }
                                }
                                if (swipeType.y != 0.0f)
                                {
                                    if (swipeType.y > 0.0f)
                                    {
                                    // MOVE UP
                                    _nextDir = Vector2.up;
                                }
                                    else
                                    {
                                    // MOVE DOWN
                                    _nextDir = - Vector2.up;
                                }
                                }
                            }

                            break;
                    }
                }
            }
           
        




      




        // if pacman is in the center of a tile
        if (Vector2.Distance(_dest, transform.position) < 0.00001f)
        {
            if (Valid(_nextDir))
            {
                _dest = (Vector2)transform.position + _nextDir;
                _dir = _nextDir;
            }
            else   // if next direction is not valid
            {
                if (Valid(_dir))  // and the prev. direction is valid
                    _dest = (Vector2)transform.position + _dir;   // continue on that direction

                // otherwise, do nothing
            }
        }
    }


    public void right() {

        _nextDir = Vector2.right;
    }
    public void left()
    {

        _nextDir = -Vector2.right;
    }
    public void up()
    {

        _nextDir = Vector2.up;
    }
    public void down()
    {

        _nextDir = -Vector2.up;
    }






    public Vector2 getDir()
    {
        return _dir;
    }

    public void UpdateScore()
    {
        killstreak++;

        // limit killstreak at 4
        if (killstreak > 4) killstreak = 4;

        Instantiate(points.pointSprites[killstreak - 1], transform.position, Quaternion.identity);
        GameManager.score += (int)Mathf.Pow(2, killstreak) * 100;

    }
}
