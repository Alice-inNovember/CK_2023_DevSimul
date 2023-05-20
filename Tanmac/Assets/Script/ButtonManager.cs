using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject[] BulletSpawner;
    private List<BulletSpawner> _bulletSpawner;

    private void Start()
    {
        _bulletSpawner = new List<BulletSpawner>();
        for (int i = 0; i < BulletSpawner.Length; i++)
        {
            _bulletSpawner.Add(BulletSpawner[i].GetComponent<BulletSpawner>());
        }
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
    public void IncreaseDifficulty()
    {
        for (int i = 0; i < _bulletSpawner.Count; i++)
        {
            _bulletSpawner[i].AddDificalty(1);
        }
    }
    public void DecreaseDifficulty()
    {
        for (int i = 0; i < _bulletSpawner.Count; i++)
        {
            _bulletSpawner[i].AddDificalty(-1);
        }
    }
}
