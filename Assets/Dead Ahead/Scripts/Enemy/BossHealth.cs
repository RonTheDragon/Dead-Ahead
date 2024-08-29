
public class BossHealth : EnemyHealth
{
    protected new void Start()
    {
        base.Start();
        OnDeath += _gameManager.EnemySpawner.BossKilled;
    }

    public override void Spawn()
    {
        base.Spawn();
        _animator.SetBool("Walk", true);
    }

    public override void Die()
    {
        _killType = KillInfo.KillType.Boss;
        base.Die();
    }
}
