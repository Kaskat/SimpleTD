using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Data;
using Tower;
using Enemy;

namespace GameCore
{
    public class LevelController : MonoBehaviour
    {
        #region Fields

        private GameParametrs _gameParametrs;
        private EnemyManager _enemyManager;
        public List<Slot> slots;

        #endregion Fields

        #region Public Methods

        public void Initialize(GameParametrs gameParametrs, EnemyManager enemyManager)
        {
            _gameParametrs = gameParametrs;
            _enemyManager = enemyManager;

            var levelGameObject = Instantiate(_gameParametrs.levelPrefab);
            var waypoints = levelGameObject.GetComponent<WaypointController>().waypoints;
            slots = levelGameObject.GetComponentsInChildren<Slot>().ToList();

            enemyManager.SetWaypoints(waypoints);
            enemyManager.EnemyDied += GetCoinFromEnemy;
        }

        #endregion Public Methods

        #region Private Methods

        private void GetCoinFromEnemy(BaseEnemy baseEnemy)
        {
            _gameParametrs.Coin += baseEnemy.Cost;
        }

        private void Start()
        {
            var waveSequence = DOTween.Sequence();
            for (int i = 0; i < _gameParametrs.maxWaveCount; i++)
            {
                var wave = _gameParametrs.wavesData[i];
                waveSequence.AppendCallback(() => StartCoroutine(_enemyManager.ActivateWave(wave)));
                waveSequence.AppendInterval(wave.duration);

                //TODO: Redesign this
                if (i < _gameParametrs.maxWaveCount - 1)
                {
                    waveSequence.AppendCallback(() => _gameParametrs.Wave += 1);
                }
            }
        }
        #endregion Private Methods
    }
}