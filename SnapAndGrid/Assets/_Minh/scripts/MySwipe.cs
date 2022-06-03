using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Common;
using Lean.Touch;

public class MySwipe : LeanSwipeBase
{
    // Start is called before the first frame update
    public PlayerManage player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        LeanTouch.OnFingerSwipe += HandleFingerSwipe;
    }
    private void OnDestroy()
    {
        LeanTouch.OnFingerSwipe -= HandleFingerSwipe;
    }
    private void HandleFingerSwipe(LeanFinger finger)
    {


        HandleFingerSwipe(finger, finger.StartScreenPosition, finger.ScreenPosition);
        Vector2 swipeVector = finger.SwipeScaledDelta;
        if (Mathf.Abs(swipeVector.y) > Mathf.Abs(swipeVector.x))
        {
            if (/*swipeVector.x < 0 && */swipeVector.y > 0)
            {
                // Debug.LogError("swipe up");
                InputInfo inputInfo = new InputInfo();
                inputInfo.inputType = InputID.SWIPE_UP;
                player.PlayerMoveControl(InputID.SWIPE_UP);



            }
            if (/*swipeVector.x > 0 &&*/ swipeVector.y < 0)
            {
                // Debug.LogError("swipe down");
                InputInfo inputInfo = new InputInfo();
                inputInfo.inputType = InputID.SWIPE_DOWN;
                player.PlayerMoveControl(InputID.SWIPE_DOWN);

            }
        }
        else
        {
            if (swipeVector.x < 0 /*&& swipeVector.y < 0*/)
            {
                // Debug.LogError("swipe left");
                InputInfo inputInfo = new InputInfo();
                inputInfo.inputType = InputID.SWIPE_LEFT;
                player.PlayerMoveControl(InputID.SWIPE_LEFT);

            }
            if (swipeVector.x > 0/* && swipeVector.y > 0*/)
            {
                // Debug.LogError("swipe right");
                InputInfo inputInfo = new InputInfo();
                inputInfo.inputType = InputID.SWIPE_RIGHT;
                player.PlayerMoveControl(InputID.SWIPE_RIGHT);

            }
        }

    }

}
public enum InputID
{
    NONE,
    SWIPE_LEFT,
    SWIPE_RIGHT,
    SWIPE_UP,
    SWIPE_DOWN,
    DRAW_L,
    DRAW_U,
    DRAW_V1,//v nguoc
    TWO_LEFT, //two fing tap on left screen
    TWO_RIGHT, //two fing tap on right screen
    HOLD
}
public struct InputInfo
{
    public InputID inputType;
}



