using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;

    [SerializeField] private PlayerHealth Player;

    [SerializeField] private HealthPooler _op;

    public void Awake()
    {
        Instance = this;
    }

    public PlayerHealth GetPlayerHealth()
    {
        return Player;
    }

    public HealthPooler GetHealthPooler()
    {
        return _op;
    }
}
