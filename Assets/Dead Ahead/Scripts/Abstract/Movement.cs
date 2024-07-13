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

    protected abstract void Movements();
}
