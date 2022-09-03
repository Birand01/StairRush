using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
 
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlayerRagdoll.OnPlayerDisableAnimation += DisableAnimation;
        BrickStack.OnPlayerVictoryAnimation += DanceAnimation;
        RaycastCollisionCheck.OnPlayerFallingAnimation += FallingAnimation;
        PlayerInput.OnMovement += RunAnimation;
        EndLineTrigger.OnPlayerReachedEndLineSadAnimation += SadIdleAnimation;
    }

    private void RunAnimation()
    {
        anim.SetFloat("Speed", 1.0f);
    }
    private void DisableAnimation()
    {
        anim.enabled = false;
    }
   
    private void FallingAnimation(bool state)
    {
        anim.SetBool("Falling", state);
      
    }
    private void SadIdleAnimation()
    {
        anim.SetTrigger("Lose");
    }
    private void DanceAnimation()
    {
        anim.SetTrigger("Victory");
    }

    private void OnDisable()
    {
        EndLineTrigger.OnPlayerReachedEndLineSadAnimation -= SadIdleAnimation;
        PlayerRagdoll.OnPlayerDisableAnimation -= DisableAnimation;
        BrickStack.OnPlayerVictoryAnimation -= DanceAnimation;
        RaycastCollisionCheck.OnPlayerFallingAnimation -= FallingAnimation;
        PlayerInput.OnMovement -= RunAnimation;
    }
}
