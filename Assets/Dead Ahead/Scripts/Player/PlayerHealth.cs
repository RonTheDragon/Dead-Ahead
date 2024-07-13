using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health , IPlayerComponent
{
    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }

    public void PlayerStart(PlayerRefs refs)
    {
        Spawn();
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
