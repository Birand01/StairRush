using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivation : MonoBehaviour
{
    private void Awake()
    {
        foreach  (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
