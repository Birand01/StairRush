using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollisionCheck : MonoBehaviour
{
    public delegate void OnPlayerFallingAnimationHandler(bool state);
    public static event OnPlayerFallingAnimationHandler OnPlayerFallingAnimation;
   


    [SerializeField] float range;
    private Rigidbody rb;
    private void Update()
    {
        rb = GetComponent<Rigidbody>();
        RayCastCollision();
    }

    private void RayCastCollision()
    {
        Vector3 direction = Vector3.down;
        Ray ray=new Ray(transform.position,transform.TransformDirection(direction* range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction* range), Color.red);
        if(Physics.Raycast(ray,out RaycastHit hit,range))
        {
            
            if(hit.collider.tag=="StairBrick")
            {
                OnPlayerFallingAnimation?.Invoke(false);
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionX| RigidbodyConstraints.FreezePositionZ;
            }
            

            if(this.transform.position.y>1f && hit.collider.tag=="Ground")
            {
                OnPlayerFallingAnimation?.Invoke(true);
            }
            else if(this.transform.position.y <1f || hit.collider.tag == "UpGround")
            {
                OnPlayerFallingAnimation?.Invoke(false);
            }
           
        }
       
    }
}
