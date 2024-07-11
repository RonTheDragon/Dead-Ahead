using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameManager _gameManager;
    private HealthPooler _pool;
    private Transform _playerPos;
    private PlayerHealth _playerHealth;

    [SerializeField] private List<SpawnerType> _spawners;
    [SerializeField] private float _distanceBehindPlayer, _minY, _maxY;



    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _pool = _gameManager.GetHealthPooler();
        _playerHealth = _gameManager.GetPlayerHealth();
        _playerPos = _playerHealth.transform;

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
        while (!_playerHealth.IsDead())
        {
            yield return new WaitForSeconds(Random.Range(spawner.MinRandomTime, spawner.MaxRandomTime));
            SpawnEnemy(spawner);
        }
    }

    private void SpawnEnemy(SpawnerType spawner)
    {
        Vector2 spawnPos = new Vector2(_playerPos.position.x-_distanceBehindPlayer,Random.Range(_minY, _maxY));
        _pool.CreateOrSpawnFromPool(spawner.EnemyTag, spawnPos, Quaternion.identity,_pool.transform).Spawn();
    }

    [System.Serializable]
    private class SpawnerType
    {
        public string EnemyTag;
        public float MinRandomTime, MaxRandomTime, FirstTimeDelay;
    }
}
