using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public GameObject retryBtn;

    void Awake()
    {
        I = this;
    }
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void gameOver()
    {
        retryBtn.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
