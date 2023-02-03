using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBlock : MonoBehaviour
{
    public Camera mainCamera;
    void Start()
    {
        
    }

    void Update()
    {
        //得到鼠标坐标
        //Debug.Log(mainCamera.ScreenToWorldPoint(Input.mousePosition));
        //Vector3 mouseToWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //mouseToWorldPosition.z = 0f;

        RaycastHit ray;
        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),out ray))
        {
            
            if(Input.GetMouseButtonDown(0))
            {
                
            }
        }

        
    }
}
