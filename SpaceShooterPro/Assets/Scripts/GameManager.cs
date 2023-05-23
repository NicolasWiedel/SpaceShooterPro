using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    bool _isGameOver = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1); // GameScene
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
