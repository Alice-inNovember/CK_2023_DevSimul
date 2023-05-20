using TMPro;
using UnityEngine;

namespace Script
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject lifeText;
        [SerializeField] private GameObject ScoreText;
        [SerializeField] private GameObject GameOver;
    
        private TextMeshProUGUI _lifeText;
        private TextMeshProUGUI _scoreText;
    
        void Start()
        {
            GameOver.SetActive(false);
            _lifeText = lifeText.GetComponent<TextMeshProUGUI>();
            _scoreText = ScoreText.GetComponent<TextMeshProUGUI>();
        }
    
        public void SetLife(int life)
        {
            _lifeText.text = "Life : " + life.ToString();
        }
    
        public void SetScore(float score)
        {
            _scoreText.text = "Time : " + ((int)score).ToString();
        }

        public void SetGameOver()
        {
            GameOver.SetActive(true);
        }
    }
}
