using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using GameCore;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        #region Events

        public event Action<BaseEnemy> EnemyDied;

        #endregion Events

        #region Fields

        //TODO: MOVE to GAMECONFIG
        [SerializeField] 
        private float spawInterval = 1.5f;
        private EnemyStorage _enemyStorage;
        private Gate _gate;
        private Vector3[] _waypoints;
        private float _wayLength;
        public List<BaseEnemy> activeEnemies;

        #endregion Fields

        #region Public Methods

        public void Initialize(EnemyStorage enemyStorage, Gate gate)
        {
            _gate = gate;
            _enemyStorage = enemyStorage;
        }

        public void SetWaypoints(Vector3[] waypoints)
        {
            _waypoints = waypoints;
            _wayLength = 0f;
            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                _wayLength += (waypoints[i] - waypoints[i + 1]).magnitude;
            }
        }

        public IEnumerator ActivateWave(WaveData waveData)
        {
            for (int i = 0; i < waveData.enemiesId.Count; i++)
            {
                int enemyCount = waveData.enemiesId[i].enemyCount;
                EnemyData enemyData = _enemyStorage.GetEnemyDataById(waveData.enemiesId[i].id);

                for (int j = 0; j < enemyCount; j++)
                {
                    var baseEnemy = Instantiate(enemyData.enemyPrefab).GetComponent<BaseEnemy>();
                    baseEnemy.Died += RemoveEnemy;
                    baseEnemy.gameObject.SetActive(false);

                    var offsetY = Vector3.up * UnityEngine.Random.Range(-0.5f, 0.5f);
                    baseEnemy.Activate(_gate, _waypoints, _wayLength, enemyData, offsetY);

                    activeEnemies.Add(baseEnemy);

                    var randomInterval = spawInterval * UnityEngine.Random.Range(0.5f, 1.3f);

                    yield return new WaitForSeconds(randomInterval);
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void RemoveEnemy(BaseEnemy baseEnemy)
        {
            EnemyDied?.Invoke(baseEnemy);
            activeEnemies.Remove(baseEnemy);
            Destroy(baseEnemy.gameObject);
        }

        #endregion Private Methods
    }
}