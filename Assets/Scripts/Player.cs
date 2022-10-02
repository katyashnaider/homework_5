using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private HashAnimation _hashAnimation;

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

        _animator.SetFloat(_hashAnimation.Speed, Vector3.ClampMagnitude(directionVector, 1).magnitude);
        _rigidbody.velocity = new Vector3(_horizontal * _speed, _rigidbody.velocity.y, _vertical * _speed);
    }
}
