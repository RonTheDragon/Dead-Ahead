using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyEnemyHealth : EnemyHealth
{

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {            
         Die();
    }

}
