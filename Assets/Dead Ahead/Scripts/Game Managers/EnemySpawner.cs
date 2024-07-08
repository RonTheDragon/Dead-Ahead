using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameManager _gameManager;
    private HealthPooler _pool;
    private Transform _playerPos;

    [SerializeField] private string _enemyTag = "Enemy";
    [SerializeField] private float _distanceBehindPlayer, _maxY, _minY, _tempCooldown;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _pool = _gameManager.GetHealthPooler();
        _playerPos = _gameManager.GetPlayer().transform;

        InvokeRepeating(nameof(SpawnEnemy), _tempCooldown, _tempCooldown);
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = new Vector2(_playerPos.position.x-_distanceBehindPlayer,Random.Range(_minY,_maxY));

        _pool.CreateOrSpawnFromPool(_enemyTag, spawnPos, Quaternion.identity,_pool.transform).Spawn();
    }
}
