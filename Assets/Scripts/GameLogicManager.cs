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
    public bool gameIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = player.position.y;
        gameIsPaused = true;
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
        if (gameIsPaused == true)
        {
            Time.timeScale = 0;
        }
        else if (gameIsPaused == false)
        {
            Time.timeScale = 1;
        }
        if ((Input.anyKeyDown) && gameIsPaused == true)
        {
            gameIsPaused = false;
        }
        if (playerIsDead)
        {
            deathScreen.SetActive(true);
            Save();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ReloadGame();
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
        else if (playerScore <= 0)
        {
            Load();
        }
        highScoreText.text = "High Score = " + Mathf.RoundToInt(highScore).ToString() + " Points";
        if (Input.GetKeyDown(KeyCode.Space) && playerIsDead)
        {
            ReloadGame();
        }
    }
    public void ReloadGame()
    {
        Debug.Log("Reload");
        SceneManager.LoadScene(0);
    }
    public void Save()
    {
        Debug.Log("Saving");
        SaveLoad.SavePlayer(this);

    }
    public void Load()
    {
        PlayerData data = SaveLoad.LoadPlayer();
        highScore = data.highScore;
    }
}
