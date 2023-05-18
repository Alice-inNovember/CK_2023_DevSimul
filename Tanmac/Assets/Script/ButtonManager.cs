using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void RetryButton()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
}
