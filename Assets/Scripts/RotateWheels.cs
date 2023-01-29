using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheels : MonoBehaviour
{
    private int rotSpeed = 1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * rotSpeed, 0, 0));
    }
}
