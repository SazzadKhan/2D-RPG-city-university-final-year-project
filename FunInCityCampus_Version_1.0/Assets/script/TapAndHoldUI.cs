using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class TapAndHoldUI : MonoBehaviour
{

    public bool isRacePressed = false;

    private MoveAnimation player;
  //  public bool isbrakePressed = false;

    void Start()
    {
        player = GameObject.Find("player").GetComponent<MoveAnimation>();
    }

    void Update()
    {
        if (isRacePressed)
        {

           player.BoostCheck = true;
          
            //Your code Here
        }

        else if (!isRacePressed)
        {


           player.BoostCheck = false;
        
        }
    }


    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }

    public void onPointerUpRaceButton()
    {
        isRacePressed = false;
    }
    public void Talk() {
        player.TalkCheck = true;

    }

}
