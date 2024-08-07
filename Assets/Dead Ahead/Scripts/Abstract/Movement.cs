using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected float _movementSpeed;
    [SerializeField] protected float _steerSpeed;
    protected Transform _movingTransform;

    protected void FixedUpdate()
    {
        Movements();
    }

    protected void FixZ()
    {
        if (_movingTransform != null)
        _movingTransform.position = new Vector3(_movingTransform.position.x, _movingTransform.position.y, _movingTransform.position.y);
    }

    protected abstract void Movements();
}
