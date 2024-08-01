using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private TheShop _shop;

    private void Start()
    {
        _shop.SetUpShop(_saveSystem.GameData);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);     
    }

    public void QuitGame()
    {
        _saveSystem.SaveGame();
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif
    }
}
