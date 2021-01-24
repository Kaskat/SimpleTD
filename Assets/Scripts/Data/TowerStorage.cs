using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "TowerStorage", menuName = "Tower/NewTowerStorage", order = 4)]
    public class TowerStorage : ScriptableObject
    {
        #region Fields

        [SerializeField] private TowerData[] _towersData;
        private Dictionary<int, TowerData> _dict;

        #endregion Fields

        #region Public Methods

        //TODO: Remove Duplicate code with EnemyStorage
        public TowerData GetTowerDataById(int id)
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
            _dict = new Dictionary<int, TowerData>(_towersData.Length);
            foreach (var towerData in _towersData)
            {
                _dict.Add(towerData.id, towerData);
            }
        }

        #endregion Private Methods
    }
}


