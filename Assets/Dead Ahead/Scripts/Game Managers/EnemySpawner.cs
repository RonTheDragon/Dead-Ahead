using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameManager _gameManager;
    private HealthPooler _pool;
    private PlayerRefs _playerRefs;
    private PlayerHealth _playerHealth;
    private Camera _playerCam;
    

    [SerializeField] private List<SpawnerType> _spawners;
    [SerializeField] private float _minY, _maxY;

    private bool _bossAlive;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _pool = _gameManager.HealthPooler;
        _playerRefs = _gameManager.PlayerRefs;
        _playerHealth = _playerRefs.PlayerHealth;
        _playerCam = _playerRefs.Camera;

        StartAllSpawners();
    }

    private void StartAllSpawners()
    {
        foreach (SpawnerType spawner in _spawners)
        {
            StartCoroutine(StartSpawner(spawner));
        }
    }

    private IEnumerator StartSpawner(SpawnerType spawner)
    {
        yield return new WaitForSeconds(spawner.FirstTimeDelay);
        while (!_playerHealth.IsDead)
        {
            yield return new WaitForSeconds(Random.Range(spawner.MinRandomTime, spawner.MaxRandomTime));
            SpawnEnemy(spawner);
            if (spawner.IsBoss)
            {
                _bossAlive = true;
                while (_bossAlive) 
                {
                    yield return null;
                }
            }
        }
    }

    public void BossKilled()
    {
        _bossAlive = false;
    }

    private void SpawnEnemy(SpawnerType spawner)
    {
        // Get the left edge position in world coordinates
        float leftEdgeX = _playerCam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 1f; // Subtract a small value for offset
        float randomY = Random.Range(_minY, _maxY);
        if (spawner.IsBoss) { randomY = (_minY + _maxY) / 2; }
        Vector2 spawnPos = new Vector2(leftEdgeX, randomY);

        _pool.CreateOrSpawnFromPool(spawner.EnemyTag, spawnPos, Quaternion.identity, _pool.transform).Spawn();
    }


    [System.Serializable]
    private class SpawnerType
    {
        public string EnemyTag;
        public float MinRandomTime, MaxRandomTime, FirstTimeDelay;
        public bool IsBoss;
    }
}
