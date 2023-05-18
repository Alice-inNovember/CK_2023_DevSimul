using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PalyerController : MonoBehaviour
{
    public GameObject lifeText;
    public GameObject ScoreText;
    public GameObject GameOver;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private int maxLife = 5;
    private TextMeshProUGUI _lifeText;
    private TextMeshProUGUI _scoreText;
    private float _score;

    private float Score
    {
        get { return _score; }
        set
        {
            _score = value;
            _scoreText.text = "Score : " + ((int)_score).ToString();
        }
    }
    private int _life;
    private int Life
    {
        get { return _life; }
        set
        {
            _life = Mathf.Clamp(value, 0, maxLife);
            if (value <= 0)
            {
                GameOver.SetActive(true);
                Time.timeScale = 0;
                Debug.Log("플레이어 사망!");
            }
            _lifeText.text = "Life : " + _life.ToString();
        }
    }

    // Update is called once per frame
    private void Start()
    {
        GameOver.SetActive(false);
        _lifeText = lifeText.GetComponent<TextMeshProUGUI>();
        _scoreText = ScoreText.GetComponent<TextMeshProUGUI>();
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
