using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;

    [SerializeField] private PlayerRefs _playerRefs;

    [SerializeField] private SaveSystem _saveSystem;

    [SerializeField] private SOweaponsList _weaponsList;

    [SerializeField] private Camera _playerCamera;

    [SerializeField] private HealthPooler _hp;

    [SerializeField] private ChunksPooler _cp;

    [SerializeField] private UIPopUpsPooler _uipup;

    [SerializeField] private EnemySpawner _enemySpawner;
    

    public void Awake()
    {
        Instance = this;
    }

    public PlayerRefs PlayerRefs => _playerRefs;
    public SaveSystem SaveSystem => _saveSystem;
    public SOweaponsList WeaponsList => _weaponsList;
    public HealthPooler HealthPooler => _hp;
    public ChunksPooler ChunksPooler => _cp;
    public UIPopUpsPooler UIPopUpsPooler => _uipup;
    public Camera PlayerCamera => _playerCamera;
    public EnemySpawner EnemySpawner => _enemySpawner;
}
