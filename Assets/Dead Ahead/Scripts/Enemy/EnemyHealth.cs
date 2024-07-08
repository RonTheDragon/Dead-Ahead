using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private EnemyMovement _enemyMovement => GetComponent<EnemyMovement>();

    public override void Spawn()
    {
        base.Spawn();
        _enemyMovement.Spawn();
    }
    public override void Die()
    {
        gameObject.SetActive(false);
    }
}
