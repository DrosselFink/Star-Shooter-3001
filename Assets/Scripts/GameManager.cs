using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1); //Current Game Scene
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Screen.fullScreen)
            {
                Screen.fullScreen = false;
            }

            else
            {
                Application.Quit();
            }

        }
        
    }

    public void GameOver()
    {
        _isGameOver = true;

       GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {

          Enemy _enemyScript = enemy.GetComponent<Enemy>();
            _enemyScript.StopShooting();
        }
    }

}
