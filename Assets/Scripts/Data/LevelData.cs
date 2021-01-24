using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level/NewLevel", order = 1)]
    public class LevelData : ScriptableObject
    {
        public int id;
        public List<WaveData> waves;
        public GameObject levelPrefab;
        public int healthPoint;
        public int coinPoint;
    }
}

