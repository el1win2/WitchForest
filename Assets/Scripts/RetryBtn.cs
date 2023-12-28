using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBtn : MonoBehaviour
{
    public void GameReStart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
