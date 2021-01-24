using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "Level/Wave", order = 2)]
    public class WaveData : ScriptableObject
    {
        public float duration;
        public float spawnIntervals;
        public List<EnemiesIdByCount> enemiesId;
    }
}

