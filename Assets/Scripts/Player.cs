using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private float _rotationSpeed = 10f;
    private float _horizontal;
    private float _vertical;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        Vector3 directionVector = new Vector3(_horizontal, 0, _vertical);

        if (directionVector.magnitude > Mathf.Abs(0.05f))
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), _rotationSpeed * Time.deltaTime);

        _animator.SetFloat("Speed", Vector3.ClampMagnitude(directionVector, 1).magnitude);
        _rigidbody.velocity = Vector3.ClampMagnitude(directionVector, 1) * _speed;
    }
}
