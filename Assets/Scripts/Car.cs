using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle
{
    private void Start()
    {
        ChangeLanes();
    }

    public override void ChangeLanes()
    {
        Debug.Log("Car ChangeLanes() called!");
        base.ChangeLanes();
    }
}
