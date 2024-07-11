using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }

    public bool TryCatchPlayer(float enemyX)
    {
        if (enemyX < transform.position.x)
        {
            Die(); return true;
        }
        return false;
    }
}
