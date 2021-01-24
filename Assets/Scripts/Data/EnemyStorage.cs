using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = "EnemyStorage", menuName = "Enemy/NewEnemyStorage", order = 1)]
    public class EnemyStorage : ScriptableObject
    {
        #region Fields

        [SerializeField] private EnemyData[] _enemyData;
        private Dictionary<int, EnemyData> _dict;

        #endregion Fields

        #region Public Methods

        //TODO: Duplicate code with EnemyStorage
        public EnemyData GetEnemyDataById(int id)
        {
            if (_dict == null)
                InitDictionary();

            if (!_dict.ContainsKey(id))
                throw new Exception("Not found id: " + id);

            return _dict[id];
        }

        #endregion Public Methods

        #region Private Methods

        private void InitDictionary()
        {
            _dict = new Dictionary<int, EnemyData>(_enemyData.Length);
            foreach (var towerData in _enemyData)
            {
                _dict.Add(towerData.id, towerData);
            }
        }

        #endregion Private Methods
    }
}

