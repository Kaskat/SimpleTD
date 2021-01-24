using System.Collections.Generic;
using UnityEngine;
using Tower;
using Data;

namespace GameCore
{
    public class TowerManager : MonoBehaviour
    {
        #region Fields

        private TowerStorage _towerStorage;
        public List<BaseTower> towers;

        #endregion Fields

        #region Public Methods

        public void Initialize(TowerStorage towerStorage, int count)
        {
            _towerStorage = towerStorage;
            towers = new List<BaseTower>(count);
        }

        public BaseTower Build(int targetId, Vector3 position)
        {
            TowerData towerData = _towerStorage.GetTowerDataById(targetId);

            //TODO: Add Pool manager
            GameObject targetGameObject = Instantiate(towerData.towerPrefab, position, Quaternion.identity);
            BaseTower baseTower = targetGameObject.GetComponent<BaseTower>();
            baseTower.Initialize(towerData);

            towers.Add(baseTower);

            return baseTower;
        }

        public void DistructBuild(BaseTower baseTower)
        {
            Destroy(baseTower.gameObject);
            towers.Remove(baseTower);
        }

        #endregion Public Methods
    }
}