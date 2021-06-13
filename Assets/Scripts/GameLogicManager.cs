using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
[System.Serializable]
public class GameLogicManager : MonoBehaviour
{
    #region Player and Scoring
    public float playerScore;
    public float highScore;
    public Text scoreText;
    public Text highScoreText;
    public Transform player;
    //this will update the player's score as they go higher on the x axis, but will also prevent the score from backsliding when they fall
    #endregion
    #region Platforms and Generation
    public GameObject platformPrefab;
    public int platformCount = 200;
    public float minY = 0.2f;
    public float maxY = 5f;
    public float levelWidth = 3f;
    #endregion
    // this region will deal with the platforms, how many there are and how wide apart they spawn. not happy withthe current system so may revisit later
    // this will increase the player's score the higher they go and then save it when they die
    public bool playerIsDead;
    public GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = player.position.y;
        playerIsDead = false;
        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < platformCount; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsDead)
        {
            deathScreen.SetActive(true);
        }
        if (player.position.y > playerScore)
        {
            playerScore = player.position.y;
            scoreText.text = playerScore.ToString("0");
        }
        scoreText.text = Mathf.RoundToInt(playerScore).ToString() + " Points";
        if (playerScore > highScore)
        {
            highScore = playerScore;
        }
        highScoreText.text = highScore.ToString();
        if (Input.GetKeyDown(KeyCode.F5) && playerIsDead)
        {
            SaveGame();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if( collision.gameObject.CompareTag("Death Field"))
        {
            playerIsDead = true;
        }
    }
    public void ReloadGame()
    {
        SceneManager.LoadScene(1);
    }
    [System.Serializable]
    class SaveData
    {
        public float savedHighScore;
    }
    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/mysavedata.dat");
        SaveData data = new SaveData();
        data.savedHighScore = highScore;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Saved the Game");
    }
    void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/mysavedata.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/mysavedata.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            highScore = data.savedHighScore;
            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.LogError("No save data!");
        }
    }
}
