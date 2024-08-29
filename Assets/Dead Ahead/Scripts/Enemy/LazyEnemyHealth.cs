using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyEnemyHealth : EnemyHealth
{
    public override void Spawn()
    {
        base.Spawn();
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.y);
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _killType = KillInfo.KillType.Runover;
        }
        Die();
    }
}
