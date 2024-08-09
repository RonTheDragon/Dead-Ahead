using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private GameManager _gameManager;
    private ChunksPooler _chunksPooler;
    private Camera _playerCam;
    private float _currentMapMaxX;
    [SerializeField] private string[] _chunks;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _chunksPooler = _gameManager.ChunksPooler;
        _playerCam = _gameManager.PlayerRefs.Camera;
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
            LevelChunk lc = _chunksPooler.SpawnFromPool(_chunks[Random.Range(0,_chunks.Length)], new Vector3(rightEdgeX, 0,0), Quaternion.identity);
            _currentMapMaxX = rightEdgeX + Random.Range(lc.Length, lc.Length+lc.RandomExtraLength);
            lc.ResetChunk();
        }
    }
}
