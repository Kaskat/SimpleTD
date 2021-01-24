using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class GameParametrs : MonoBehaviour
    {
        #region Constants

        private const int startWave = 0;

        #endregion Constants

        #region Fields

        public event Action<int> HealthChanged;
        public event Action<int> CoinChanged;
        public event Action<int> WaveChanged;

        public bool IsControllingPanelOpen;

        public int maxWaveCount;
        public GameObject levelPrefab;
        public List<WaveData> wavesData;
        public Sprite sellSprite;

        [SerializeField] private int _healthPoint;
        [SerializeField] private int _coinPoint;
        [SerializeField] private int _currentWave;

        #endregion Fields

        #region Properties

        public int Health
        {
            get
            {
                return _healthPoint;
            }
            set
            {
                _healthPoint = value;
                HealthChanged?.Invoke(value);
            }
        }

        public int Coin
        {
            get
            {
                return _coinPoint;
            }
            set
            {
                _coinPoint = value;
                CoinChanged?.Invoke(value);
            }
        }

        public int Wave
        {
            get
            {
                return _currentWave;
            }
            set
            {
                _currentWave = value;
                WaveChanged?.Invoke(value);
            }
        }

        #endregion Properties

        #region Public Methods

        public void Initialize(LevelData levelData)
        {
            Health = levelData.healthPoint;
            Coin = levelData.coinPoint;
            Wave = startWave;

            maxWaveCount = levelData.waves.Count;
            levelPrefab = levelData.levelPrefab;
            wavesData = levelData.waves;

            IsControllingPanelOpen = false;
        }

        #endregion Public Methods

        #region MonoBehaviour

        #if UNITY_EDITOR

        private void OnValidate()
        {
            Health = _healthPoint;
            Coin = _coinPoint;
        }

        #endif

        #endregion MonoBehaviour
    }
}

