using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    public delegate void OnPlayerDisableAnimationHandler();
    public static event OnPlayerDisableAnimationHandler OnPlayerDisableAnimation;


    private Rigidbody[] _ragdollRigidbodies;
    private Collider[] _colliders;
    private Rigidbody rb;
    private Collider playerCollider;
    private void Awake()
    {
        playerCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>();
        DisableRagdoll();
    }
    private void OnEnable()
    {
        Obstacle.OnPlayerRagdoll += EnableRagdoll;
    }

    private void DisableRagdoll()
    {
        foreach (Rigidbody rb in _ragdollRigidbodies)
        {
            rb.isKinematic = true;
        
        }
        rb.isKinematic = false;
        foreach (Collider collider in _colliders)
        {
            collider.enabled = false;
        }
        playerCollider.enabled = true;
        
       
    }

    private void EnableRagdoll()
    {
        OnPlayerDisableAnimation?.Invoke();
        foreach (Rigidbody rb in _ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
        foreach (Collider collider in _colliders)
        {
            collider.enabled = true;
        }
        rb.isKinematic = false;
        rb.AddForce(new Vector3(0,300,-200),ForceMode.Impulse);
      
    }
    private void OnDisable()
    {
        Obstacle.OnPlayerRagdoll -= EnableRagdoll;
    }
}
