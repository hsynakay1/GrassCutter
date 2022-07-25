using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Transform player;      
    [SerializeField] private Vector3 offset;        
    [SerializeField] private float timeSmooth;      



    
    void Update()
    {
        
        Vector3 cameraPosition = Vector3.Lerp(transform.position, player.position + offset, timeSmooth * Time.deltaTime);

        transform.position = cameraPosition; 
    }

    public void SetOffset(Vector3 offsetValue) 
    {
        offset += offsetValue;
    }



}
