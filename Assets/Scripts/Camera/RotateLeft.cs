using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLeft : MonoBehaviour
{
    public delegate void OnCameraTurnLeftEventHandler(float value);
    public static event OnCameraTurnLeftEventHandler OnCameraTurnLeft;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TURN LEFT");
        OnCameraTurnLeft?.Invoke(-90);
    }
}
