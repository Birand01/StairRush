using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerTriggers : MonoBehaviour
{
    private Transform initialBrickPlace;
    private void Awake()
    {
        initialBrickPlace = GameObject.FindGameObjectWithTag("InitialBrickPlace").transform;
    }
    private void Start()
    {
        BrickStack.Instance.bricks.Add(initialBrickPlace);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FreeBrick"))
        {
            other.transform.parent = initialBrickPlace;
            //other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
            other.tag = "CollectedBrick";
            BrickStack.Instance.bricks.Add(other.transform);

        }

    }


}
