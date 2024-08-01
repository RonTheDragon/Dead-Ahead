using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    private string _filePath;
    private GameData _gameData;

    private void Awake()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "savefile.json");
    }

    private void OnEnable()
    {
        _gameData = Load();
    }

    private void OnDisable()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(_gameData);
        File.WriteAllText(_filePath, json);
    }

    private GameData Load()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            return data;
        }
        return new GameData();
    }

    public void ClearSave()
    {
        _gameData = new GameData();
        SaveGame();
        SceneManager.LoadScene(0);
    }

    public GameData GameData => _gameData;
}
