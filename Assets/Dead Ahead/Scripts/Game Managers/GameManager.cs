using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;

    [SerializeField] private PlayerRefs Player;

    [SerializeField] private HealthPooler _hp;

    [SerializeField] private ChunksPooler _cp;

    public void Awake()
    {
        Instance = this;
    }

    public PlayerRefs PlayerRefs => Player;
    public HealthPooler HealthPooler => _hp;
    public ChunksPooler ChunksPooler => _cp;

}
