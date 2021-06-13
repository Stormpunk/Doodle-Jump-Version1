using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Points : MonoBehaviour
{
    #region Player and Scoring
    public float playerScore;
    public float highScore;
    public Text scoreText;
    public Text highScoreText;
    public Transform player;
    //this will update the player's score as they go higher on the x axis, but will also prevent the score from backsliding when they fall
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        playerScore = player.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
