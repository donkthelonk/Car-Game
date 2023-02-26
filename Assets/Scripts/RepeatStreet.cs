using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatStreet : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    private Vector3 startPos;
    private float repeatLength;

    // Start is called before the first frame update
    void Start()
    {
        InitializeStreet();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void InitializeStreet()
    {
        startPos = transform.position;
        repeatLength = GetComponent<BoxCollider>().size.z / 2;
    }

    void UpdatePosition()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < startPos.z - repeatLength)
        {
            transform.position = startPos;
        }
    }
}
