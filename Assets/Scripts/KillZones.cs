using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZones : MonoBehaviour
{
    public GameObject gameManager;
    //allows us to access the functions inside our game manager.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Death");
        gameManager.GetComponent<GameLogicManager>().playerIsDead = true;
        collision.gameObject.SetActive(false);
    }
}
