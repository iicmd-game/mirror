using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public CheckPoint cp;
    public Faith_controller fc;
    public void Load()
    {
        PlayerPrefs.DeleteKey("MESave");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Load();
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Save()
    {
        string key = "MESave";
        SaveData data = new SaveData();
        data.playerPosition = cp.CheckPointPosition().transform.position;
        data.levelNumber = SceneManager.GetActiveScene().buildIndex;
        data.isRight = fc.isRight;

        string value = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(key, value);

        PlayerPrefs.Save();
    }

    public void LoadFromSave()
    {
        string key = "MESave";
        if (PlayerPrefs.HasKey(key))
        {
            string value = PlayerPrefs.GetString(key);
            SaveData data = JsonUtility.FromJson<SaveData>(value);
            SceneManager.LoadScene(data.levelNumber);
        }
        else Load();
    }
}
