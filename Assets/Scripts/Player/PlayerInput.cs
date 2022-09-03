using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void OnGameStartEventHandler(bool state);
    public static event OnGameStartEventHandler OnGameStart;

    public delegate void OnBrickStairEventHandler(bool state);
    public static event OnBrickStairEventHandler OnBrickStair;

    public delegate void OnMovementEventHandler();
    public static event OnMovementEventHandler OnMovement;

   


    private void Update()
    {
        if(!Obstacle.Instance.fail && !EndLineTrigger.Instance.finishLineReached)
        {
            HandleGameStart();
            HandleBrickStair();
        }
       
    }

    private void HandleGameStart()
    {
        if(Input.GetMouseButton(0) )
        {
          
            OnMovement?.Invoke();
            OnGameStart?.Invoke(true);
        }
    }
    private void HandleBrickStair()
    {
        if (Input.GetMouseButtonDown(0) && BrickStack.Instance.bricks.Count>1 )
        {
          
            OnBrickStair?.Invoke(true);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            OnBrickStair?.Invoke(false);
        }
    }
}
