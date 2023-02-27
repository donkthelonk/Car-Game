using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int mass;

    // Start is called before the first frame update
    void Start()
    {
        Honk();
        Move();
        ChangeLanes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ChangeLanes()
    {
        Debug.Log("Vehicle ChangeLanes() called!");
    }

    void Honk()
    {
        Debug.Log("Vehicle Honk() called!");
    }

    void Move()
    {
        Debug.Log("Vehicle Move() called!");
    }
}
