using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;

    [SerializeField] private GameObject Player;

    [SerializeField] private HealthPooler _op;

    public void Awake()
    {
        Instance = this;
    }

    public GameObject GetPlayer()
    {
        return Player;
    }

    public HealthPooler GetHealthPooler()
    {
        return _op;
    }
}
