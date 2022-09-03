using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickStack : MonoBehaviour
{
    public delegate void OnPlayerBrickAmountZeroHandler();
    public delegate void OnPlayerVictoryAnimationHandler();
    public static event OnPlayerVictoryAnimationHandler OnPlayerVictoryAnimation;
    public static event OnPlayerBrickAmountZeroHandler OnPlayerBrickAmountZero;



    public List<Transform> bricks=new List<Transform>();
    [SerializeField] Transform stairBase,stairBrickGarbageParent,initialBrickPlace;
    [SerializeField] private float brickDistance, swipeSpeed;
    public static BrickStack Instance { get; private set; }
    private void Awake()
    {
        bricks.Add(initialBrickPlace);
        if (Instance==null)
            Instance = this;
    }
    private void OnEnable()
    {

        EndLineTrigger.OnPlayerReachedEndLine += BrickRoadPlacement;
        PlayerInput.OnBrickStair += BrickStairPlacement;
    }

    private void Update()
    {
        BrickStacking();

    }

    /// <summary>
    /// THIS PIECE OF CODE HELPS TO STACK BRICKS WHICH ARE LOCATED ON THE GROUND
    /// </summary>
    private void BrickStacking()
    {
        if (bricks.Count > 1)
        {
            for (int i = 1; i < bricks.Count; i++)
            {
                var firstBrick = bricks.ElementAt(i - 1);
                var secondBrick = bricks.ElementAt(i);
                var desiredDistance = Vector3.Distance(secondBrick.position, firstBrick.position);
                if (desiredDistance <= brickDistance)
                {
                    secondBrick.position = new Vector3(secondBrick.position.x, Mathf.Lerp(secondBrick.position.y, firstBrick.position.y + 0.3f, swipeSpeed * Time.deltaTime),
                        Mathf.Lerp(secondBrick.position.z, firstBrick.position.z, swipeSpeed * Time.deltaTime));
                }
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------
  
    /// <summary>
    /// THIS PIECE OF CODE HELPS TO CONSTRUCT STAIR VIA COROUTINE
    /// </summary>
    /// <param name="state"></param>
   

    private void BrickStairPlacement(bool state)
    {
        if (bricks.Count > 1 && state)
        {

            StartCoroutine(BrickStairPlacementCoroutine());

        }
        else
        {
            StopAllCoroutines();
        }
           
      
    }
    IEnumerator BrickStairPlacementCoroutine()
    {
        int brickCount = bricks.Count;
        for (int i =1; i < brickCount; i++)
        {
            Transform stairBrick = bricks.ElementAt(1).transform;
            stairBrick.transform.tag = "StairBrick";
            stairBrick.parent = stairBrickGarbageParent;
            stairBrick.position = new Vector3(
              stairBase.transform.position.x,
             stairBase.transform.position.y+0.1f,
              stairBase.transform.position.z);
            stairBrick.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            this.transform.position=new Vector3(transform.position.x,transform.position.y+0.25f, transform.position.z);
            bricks.RemoveAt(1);
            yield return new WaitForSecondsRealtime(0.06f);
        }    
    }
   

    //------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// THIS PIECE OF CODE HELPS TO CONSTRUCT A ROAD DURING THE LEVEL END OCCASION
    /// </summary>

    private void BrickRoadPlacement()
    {
        StartCoroutine(BrickRoadPlacementCoroutine());
    }
    IEnumerator BrickRoadPlacementCoroutine()
    {
        int brickCount = bricks.Count;
        for (int i = 1; i < brickCount; i++)
        {
           
            Transform stairBrick = bricks.ElementAt(1).transform;
            stairBrick.transform.tag = "LineBrick";
            stairBrick.parent = stairBrickGarbageParent;
            stairBrick.gameObject.AddComponent<Rigidbody>().isKinematic = true;
            stairBrick.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            stairBrick.position = new Vector3(
              this.transform.position.x,
             this.transform.position.y-0.1f,
              this.transform.position.z+1.5f);
            bricks.RemoveAt(1);
            if(bricks.Count<=1)
            {
                OnPlayerVictoryAnimation?.Invoke();
                OnPlayerBrickAmountZero?.Invoke();
            }
            yield return new WaitForSecondsRealtime(0.07f);
        }
    }
   
  
   //----------------------------------------------------------------------------------------------------------------------------
    

    private void OnDisable()
    {
        EndLineTrigger.OnPlayerReachedEndLine -= BrickRoadPlacement;
        PlayerInput.OnBrickStair -= BrickStairPlacement;
    }
}
