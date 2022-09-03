using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LineBrickTrigger : MonoBehaviour
{
    [SerializeField] GameObject place;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("LineBrick"))
        {
            
            Debug.Log("Girdi");

            place.SetActive(true);
            place.transform.DOMoveY(1.63f, 0.7f);
        }
    }
}
