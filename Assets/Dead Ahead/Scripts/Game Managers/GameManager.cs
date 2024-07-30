using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;

    [SerializeField] private PlayerRefs _playerRefs;

    [SerializeField] private Camera _playerCamera;

    [SerializeField] private HealthPooler _hp;

    [SerializeField] private ChunksPooler _cp;

    [SerializeField] private DamageCounterPooler _dcp;

    public void Awake()
    {
        Instance = this;
    }

    public PlayerRefs PlayerRefs => _playerRefs;
    public HealthPooler HealthPooler => _hp;
    public ChunksPooler ChunksPooler => _cp;
    public DamageCounterPooler DamageCounterPooler => _dcp;
    public Camera PlayerCamera => _playerCamera;

}
