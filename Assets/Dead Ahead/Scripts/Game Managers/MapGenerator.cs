using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private GameManager _gameManager;
    private ChunksPooler _chunksPooler;
    private Camera _playerCam;
    private float _currentMapMaxX;
    [SerializeField] private SpriteRenderer _startingRoad;
    private SpriteRenderer _currentRoad;
    [SerializeField] private string _road;
    [SerializeField] private string[] _chunks;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _chunksPooler = _gameManager.ChunksPooler;
        _playerCam = _gameManager.PlayerRefs.Camera;
        _currentRoad = _startingRoad;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        float rightEdgeX = _playerCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1f;
        if (rightEdgeX > _currentMapMaxX)
        {
            LevelChunk lc = (LevelChunk)_chunksPooler.SpawnFromPool(_chunks[Random.Range(0,_chunks.Length)], new Vector3(rightEdgeX, 0,0), Quaternion.identity);
            _currentMapMaxX = rightEdgeX + Random.Range(lc.Length, lc.Length+lc.RandomExtraLength);
            lc.ResetChunk();
        }

        rightEdgeX = _playerCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float roadEdge = _currentRoad.transform.position.x + _currentRoad.bounds.extents.x*2;
        if (roadEdge < rightEdgeX+ _currentRoad.bounds.extents.x)
        {
            _currentRoad = ((RoadChunk)_chunksPooler.SpawnFromPool(_road, new Vector3(roadEdge, _currentRoad.transform.position.y, _currentRoad.transform.position.z),
                Quaternion.identity)).Sprite;
        }

    }
}
