using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField][ReadOnly] private float _currentHealth;
    [SerializeField] private float _maxHealth;


    public virtual void Spawn()
    {
        _currentHealth = _maxHealth;
    }
    public abstract void Die();
}
