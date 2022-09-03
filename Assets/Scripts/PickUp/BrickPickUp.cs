using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPickUp : PickUp
{
    protected override void OnPickUp(PlayerMovement player)
    {
        BrickStack brickStack = player.GetComponent<BrickStack>();
        if(brickStack!=null)
        {
            this.transform.parent = GameObject.FindGameObjectWithTag("InitialBrickPlace").transform;
            this.tag = "CollectedBrick";
            BrickStack.Instance.bricks.Add(this.transform);
        }
    }
}
