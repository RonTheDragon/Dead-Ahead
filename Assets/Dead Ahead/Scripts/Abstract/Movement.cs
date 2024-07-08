using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected float _movementSpeed;
    [SerializeField] protected float _steerSpeed;
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    protected Transform _movingTransform;

    protected void Start()
    {
        _movingTransform = _rigidbody2D.transform;
    }

    protected void FixedUpdate()
    {
        StuckCheck();
        Movements();
    }

    protected abstract void Movements();

    protected void StuckCheck()
    {
        if (_rigidbody2D.velocity.x == 0)
        {
            GotStuck();
        }
    }

    protected abstract void GotStuck();
}
