using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update() // ---> R'ye basarak oyunu restartladığımız yer.
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true) // ---> Sadece _isGameOver true olduğunda R'ye basarsam.
        {
            SceneManager.LoadScene(0); // ---> Hangi sahneyi göstermek isteğimiz yer.
        }
    }
    
    public void GameOver()
    {
        _isGameOver = true;
    }
}
