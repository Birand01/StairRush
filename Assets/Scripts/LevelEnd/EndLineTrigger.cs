using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLineTrigger : MonoBehaviour
{
    public delegate void OnPlayerReachedEndLineHandler();
    public static event OnPlayerReachedEndLineHandler OnPlayerReachedEndLine;

    public delegate void OnPlayerReachedEndLineSadAnimationHandler();
    public static event OnPlayerReachedEndLineSadAnimationHandler OnPlayerReachedEndLineSadAnimation;

    public bool finishLineReached;
    public static EndLineTrigger Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        finishLineReached = false;
    }
    private void OnTriggerEnter(Collider other)
    {
      
        OnPlayerReachedEndLine?.Invoke();
        finishLineReached = true;
        if(BrickStack.Instance.bricks.Count<=1)
        {
            finishLineReached = true;

            OnPlayerReachedEndLineSadAnimation?.Invoke();
        }
    }
}
