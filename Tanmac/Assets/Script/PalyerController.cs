using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PalyerController : MonoBehaviour
{
    [SerializeField] private GameObject uiGameObject;
    [SerializeField] private float speed = 10;
    [SerializeField] private int maxLife = 5;
    
    private UiManager _uiManager;
    [SerializeField] private int _life;
    private float _score;
    private float Score
    {
        get { return _score; }
        set
        {
            _score = value;
            _uiManager.SetScore(_score);
        }
    }
    private int Life
    {
        get { return _life; }
        set
        {
            _life = Mathf.Clamp(value, 0, maxLife);
            if (value <= 0)
            {
                Time.timeScale = 0;
                _uiManager.SetGameOver();
            }
            _uiManager.SetLife(_life);
        }
    }

    private void Start()
    {
        _uiManager = uiGameObject.GetComponent<UiManager>();
        Life = maxLife;
        Life = maxLife;
        Score = 0;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, v, 0) * (speed * Time.deltaTime));
        Score += Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            Debug.Log("총알 맞음");
            Life -= 1;
        }
    }
}
