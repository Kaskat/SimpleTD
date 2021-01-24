using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Tower;

namespace GameCore
{
    public class Field : MonoBehaviour
    {
        #region Fields

        private TowerManager _towerManager;
        private EnemyManager _enemyManager;
        private Gate _gate;
        private List<BaseTower> _towers;
        private List<BaseEnemy> _enemies;
        private Vector3[] _waypoints;

        #endregion Fields

        #region Public Methods

        public void Initialize(TowerManager towerManager, EnemyManager enemyManager, Gate gate)
        {
            _towerManager = towerManager;
            _enemyManager = enemyManager;
            _gate = gate;

            _towers = _towerManager.towers;
            _enemies = _enemyManager.activeEnemies;
        }

        #endregion Public Methods

        #region MonoBehaviour

        private void Update()
        {
            _towers = _towerManager.towers;
            _enemies = _enemyManager.activeEnemies;

            if (_towers != null && _enemies != null && _towers.Count > 0 && _enemies.Count > 0)
            {
                foreach (var tower in _towers)
                {
                    tower.ClearEnemies();
                    var enemyInRange = new List<BaseEnemy>();

                    foreach (var enemy in _enemies)
                    {
                        var relativePosition = tower.gameObject.transform.position - enemy.gameObject.transform.position;
                        if(relativePosition.magnitude <= tower.AttackRange)
                        {
                            enemyInRange.Add(enemy);
                        }
                    }

                    if(enemyInRange.Count > 0)
                    {
                        tower.SetEnemies(enemyInRange);
                    }
                }
            }
        }

        #endregion MonoBehaviour
    }
}

