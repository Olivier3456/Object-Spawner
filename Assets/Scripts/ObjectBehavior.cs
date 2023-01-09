using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{

    private float _minSpeed = 1;
    private float _maxSpeed = 20;

    private float _minRotationSpeed = 10;
    private float _maxRotationSpeed = 180;

    private Vector3 _movement;
    private Vector3 _rotation;

    void Start()
    {
        _movement = SetMovementOfObject();
        _rotation = SetRotationOfObject();
    }

    public Vector3 SetRotationOfObject()
    {
        float speed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;
    }


    public Vector3 SetMovementOfObject()
    {
        float speed = Random.Range(_minSpeed, _maxSpeed);
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;
    }

    void Update()
    {
        transform.position += _movement * Time.deltaTime;
        transform.Rotate(_rotation * Time.deltaTime);
    }
}
