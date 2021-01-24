using UnityEngine;
using TMPro;
using Data;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI cointText;
        [SerializeField] private TextMeshProUGUI waveText;

        private GameParametrs _gameParametrs;

        #endregion Fields

        #region Public Methods

        public void Initialize(GameParametrs gameParametrs)
        {
            _gameParametrs = gameParametrs;

            _gameParametrs.HealthChanged += OnHealthChanged;
            _gameParametrs.CoinChanged += OnCoinChanged;
            _gameParametrs.WaveChanged += OnWaveChanged;

            healthText.text = _gameParametrs.Health.ToString();
            cointText.text = _gameParametrs.Coin.ToString();
            waveText.text = (_gameParametrs.Wave + 1) + "/" + _gameParametrs.maxWaveCount;
        }

        public void OnLevelComplete()
        {
            _gameParametrs.HealthChanged -= OnHealthChanged;
            _gameParametrs.CoinChanged -= OnCoinChanged;
            _gameParametrs.WaveChanged -= OnWaveChanged;
        }

        public void OnWaveChanged(int value)
        {
            waveText.text = (_gameParametrs.Wave + 1) + "/" + _gameParametrs.maxWaveCount;
        }

        public void OnHealthChanged(int value)
        {
            healthText.text = value.ToString();
        }

        public void OnCoinChanged(int value)
        {
            cointText.text = value.ToString();
        }

        #endregion Public Methods
    }
}

