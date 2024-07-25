using UnityEngine;

public class PlayerHealth : Health , IPlayerComponent
{
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private float _deathMenuDelay;


    public override void Die()
    {
        base.Die();
        _playerMovement.Die();
        Invoke(nameof(DeathMenu), _deathMenuDelay);
    }

    private void DeathMenu()
    {
        Time.timeScale = 0;
        _deathMenu.SetActive(true);
    }

    public void PlayerStart(PlayerRefs refs)
    {
        Spawn();
        _playerMovement = refs.PlayerMovement;
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
