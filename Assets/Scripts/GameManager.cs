using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameManager : MonoBehaviour
{
    public static int life=3;
    public static float money=1000;

    private void Update()
    {
        if (life == 0) { GameOver(); }
    }
   
    private void Start()
    {
        
    }
    void PauseGame()
    {

    }
    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
