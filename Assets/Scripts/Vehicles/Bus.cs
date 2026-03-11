using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Bus : Vehicle
{
    private Rigidbody vehicleRb;

    // Start is called before the first frame update
    void Start()
    {
        vehicleRb = GetComponent<Rigidbody>();
        damageAmount = 3;
        pointValue = 30;
    }

}
