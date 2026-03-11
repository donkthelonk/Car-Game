using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE derived class
class Car : Vehicle
{
    private int number = 0;

    // ENCAPSULATION private variable setter
    public void SetNumber(int newNumber)
    {
        number = newNumber;
    }

    // ENCAPSULATION private variable getter
    public int GetNumber()
    {
        return number;
    }

}
