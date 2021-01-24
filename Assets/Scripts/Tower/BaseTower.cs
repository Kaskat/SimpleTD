using System.Collections.Generic;
using UnityEngine;
using Data;
using Enemy;

namespace Tower
{
    public abstract class BaseTower : MonoBehaviour
    {
        #region Properties

        public int Id => _towerData.id;

        #endregion Properties

        #region Fields

        public float AttackRange => _towerData.range;

        protected List<BaseEnemy> _enemiesInRange;
        protected TowerData _towerData;
        protected bool isCanAttack;
        protected float attackTimer;

        #endregion Fields

        #region MonoBehaviour

        private void Update()
        {
            if (_enemiesInRange != null && _enemiesInRange.Count > 0)
            {
                Attack();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_towerData == null)
                return;

            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, _towerData.range);
        }
#endif

        #endregion MonoBehaviour

        #region Protected Methods

        protected abstract void Attack();

        //TODO: Order by successful list
        protected BaseEnemy FindDangerousEnemy()
        {
            BaseEnemy theMostDangirousEnemy = _enemiesInRange[0];
            for (int i = 1; i < _enemiesInRange.Count; i++)
            {
                if (_enemiesInRange[i].Succesfull > theMostDangirousEnemy.Succesfull)
                {
                    theMostDangirousEnemy = _enemiesInRange[i];
                }
            }

            return theMostDangirousEnemy;
        }

        #endregion Protected Methods

        #region Public Methods

        public void Initialize(TowerData towerData)
        {
            _towerData = towerData;
        }

        public virtual void Deactivate()
        {
            //TODO: Back to Pool
            Destroy(gameObject);
        }
        public void SetEnemies(List<BaseEnemy> enemies)
        {
            _enemiesInRange = enemies;
        }

        public void ClearEnemies()
        {
            _enemiesInRange?.Clear();
        }

        #endregion Public Methods
    }
}

