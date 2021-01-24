using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = "TowerData", menuName = "Tower/NewTowerData", order = 3)]
    public class TowerData : ScriptableObject
    {
        public int id;
        public int price;
        public int damage;
        public bool isLocked;
        public float range;
        public float shootInterval;
        public float shellFlyDuration;
        public Sprite emblem;
        public TowerData[] nextTowerUpgrade;
        public GameObject towerPrefab;
    }
}

