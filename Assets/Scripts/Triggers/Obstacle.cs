using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Linq;

public class Obstacle : MonoBehaviour
{
    public delegate void OnLevelFailEventHandler(bool state);
    public delegate void OnPlayerRagdollEventHandler();
    
    public static event OnLevelFailEventHandler OnLevelFail;
    public static event OnPlayerRagdollEventHandler OnPlayerRagdoll;

    public delegate void OnLevelRestartEventHandler();
    public static event OnLevelRestartEventHandler OnLevelRestart;

    [SerializeField] GameObject basket;
    [SerializeField] Transform garbageBricks;
    public bool fail;
    public static Obstacle Instance { get; private set; }
    private void Awake()
    {
        fail = false;
        if (Instance == null)
            Instance = this;

    }
    private void OnTriggerEnter(Collider other)
    {
      
        basket.gameObject.transform.parent = null;
        basket.GetComponent<Rigidbody>().isKinematic = false;
        basket.GetComponent<BoxCollider>().enabled = true;
        this.gameObject.GetComponent<MeshCollider>().isTrigger = false;
        OnLevelFail?.Invoke(false);
        OnPlayerRagdoll?.Invoke();
        OnLevelRestart?.Invoke();
        fail = true;
           
        int brickAmount = BrickStack.Instance.bricks.Count;
        for (int i = 1; i < brickAmount; i++)
        {
            BrickStack.Instance.bricks[i].transform.parent = null; 
            BrickStack.Instance.bricks[i].gameObject.AddComponent<Rigidbody>().isKinematic = false;
            BrickStack.Instance.bricks[i].gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,-3), ForceMode.Impulse);
            BrickStack.Instance.bricks[i].gameObject.GetComponent<BrickPickUp>().enabled = false;
           BrickStack.Instance.bricks[i].gameObject.GetComponent<BoxCollider>().isTrigger = false;
           
        }
        BrickStack.Instance.bricks.Clear();

    }

   
}
