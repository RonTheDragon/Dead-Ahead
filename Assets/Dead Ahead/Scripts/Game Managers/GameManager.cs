using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;

    [SerializeField] private GameObject Player;

    public void Awake()
    {
        Instance = this;
    }

    public GameObject GetPlayer()
    {
        return Player;
    }

}
