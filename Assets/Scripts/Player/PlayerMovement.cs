using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    private bool gameState;
    private Rigidbody rb;
    private void OnEnable()
    {
        BrickStack.OnPlayerBrickAmountZero += EndLineStoppage;
        EndLineTrigger.OnPlayerReachedEndLineSadAnimation+=EndLineStoppage; 
        Obstacle.OnLevelFail+= LevelFail;
        PlayerInput.OnGameStart += IsGameStart;
       
    }
    private void Awake()
    {
        rb=GetComponent<Rigidbody>();
    }

    private void Update()
    {    
        ForwardMovement();
        
    }

    private void IsGameStart(bool state)
    {
        gameState = state;
    }
    private void ForwardMovement()
    {
        if(gameState)
        {
            
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
       
    }
    //private void DownwardMovement(bool state)
    //{
    //    if (gameState)
    //    {
    //        rb.velocity = new Vector3(transform.position.x, 0f, transform.position.z);
    //    }
    //}
    private void EndLineStoppage()
    {
        gameState = false;
        rb.velocity = Vector3.zero;
    }

    private void LevelFail(bool state)
    {
        gameState = state;
    }
  

    
  

    private void OnDisable()
    {
        EndLineTrigger.OnPlayerReachedEndLineSadAnimation -= EndLineStoppage;
        BrickStack.OnPlayerBrickAmountZero -= EndLineStoppage;
        Obstacle.OnLevelFail -= LevelFail;
        PlayerInput.OnGameStart -= IsGameStart;
    }
}
