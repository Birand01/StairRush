using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRight : MonoBehaviour
{
    public delegate void OnCameraTurnRightEventHandler();
    public static event OnCameraTurnRightEventHandler OnCameraTurnRight;
    [SerializeField] Transform player,destination;
    [SerializeField] float rotationSpeed;
    private float rotationValue;
    private bool triggerEnter;
    private void Awake()
    {
        triggerEnter = false;
        rotationValue = 0;
       
    }
    private void Update()
    {
        if(triggerEnter)
        {

            player.localRotation = Quaternion.Slerp(player.localRotation, Quaternion.Euler(0, rotationValue * 2, 0), rotationSpeed * Time.deltaTime);
            //Vector3 targetDir = destination.position - player.position;
            //float angle = Vector3.Angle(targetDir, player.up);
            //player.rotation = Quaternion.Slerp(player.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.deltaTime);
        }

      
     
    }
    private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
           
            Debug.Log("TURN RIGHT");
            triggerEnter = true;
            rotationValue++;
          
           
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        triggerEnter = false;
    }


}
