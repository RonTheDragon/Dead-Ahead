using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    [SerializeField] private float _length;
    [SerializeField] private float _randomExtralength;
    [SerializeField] private Transform _lazyEnemiesSpots;
    [SerializeField] private List<string> _enemyTypes;
    private List<GameObject> _lazyEnemies = new List<GameObject>();

    private GameManager _gameManager;
    private HealthPooler _healthPooler;

    public void ResetChunk()
    {
        if (_gameManager == null)
        {
            _gameManager = GameManager.Instance;
            _healthPooler = _gameManager.HealthPooler;
        }
        SpawnLazyEnemies();
    }

    public float Length => _length;
    public float RandomExtraLength => _randomExtralength;

    private void SpawnLazyEnemies()
    {
        ClearLazyEnemies();

        if (_lazyEnemiesSpots != null)
        {
            foreach (Transform t in _lazyEnemiesSpots)
            {
                if (Random.Range(0, 2) == 0)
                {
                    Health h = _healthPooler.CreateOrSpawnFromPool(_enemyTypes[Random.Range(0, _enemyTypes.Count)], t.position, Quaternion.identity);
                    h.Spawn();
                    _lazyEnemies.Add(h.gameObject);
                }
            }
        }
    }

    private void ClearLazyEnemies()
    {
        if (_lazyEnemies.Count > 0)
        {
            foreach (GameObject enemy in _lazyEnemies)
            {
                enemy.gameObject.SetActive(false);
            }
            _lazyEnemies.Clear();
        }
    }

    private void OnDrawGizmos()
    {
        GizmoDrawLine(Color.green, 0);
        GizmoDrawLine(Color.cyan, _length);
        GizmoDrawLine(Color.red, _length + _randomExtralength);
    }

    private void GizmoDrawLine(Color color, float x)
    {
        Gizmos.color = color;

        // Calculate the start and end points for the line
        Vector3 start = new Vector3(transform.position.x + x, transform.position.y - 3.2f, transform.position.z);
        Vector3 end = new Vector3(transform.position.x + x, transform.position.y + 1.8f, transform.position.z);

        // Draw the line
        Gizmos.DrawLine(start, end);
    }
}
