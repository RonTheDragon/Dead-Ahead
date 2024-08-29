using UnityEngine;

public class PlayerHealth : Health , IPlayerComponent
{
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private float _deathMenuDelay;
    private Animator _playerAnimator;
    private PlayerCombat _playerCombat;
    private PlayerScoreSystem _playerScoreSystem;
    private Collider2D _playerCollider;


    public override void Die()
    {
        base.Die();
        _playerMovement.Die();
        Invoke(nameof(DeathMenu), _deathMenuDelay);
        _playerAnimator.SetBool("Death",true);
        _playerAnimator.SetBool("Jump", false);
        _playerCollider.enabled = false;
        _playerCombat.StopShootAnimation();
    }

    private void DeathMenu()
    {
        Time.timeScale = 0;
        _playerScoreSystem.ScoreBoard();
        _deathMenu.SetActive(true);
    }

    public void PlayerStart(PlayerRefs refs)
    {
        Spawn();
        _playerMovement = refs.PlayerMovement;
        _playerAnimator = refs.PlayerAnimator;
        _playerCombat = refs.PlayerCombat;
        _playerScoreSystem = refs.PlayerScoreSystem;
        _playerCollider = refs.PlayerCollider;
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
