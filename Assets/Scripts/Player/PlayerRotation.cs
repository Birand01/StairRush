using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    [SerializeField] float rotationLerpSpeed;
   
    private void Update()
    {
        RotateLeft.OnCameraTurnLeft += Rotation;
       
    }

    private void Rotation(float value)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, value, 0), Time.deltaTime * rotationLerpSpeed);
    }

    private void OnDisable()
    {
       
        RotateLeft.OnCameraTurnLeft -= Rotation;
    }
}
