using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData_Type", menuName = "Enemy/CreateEnemyData", order = 5)]
public class EnemyData : ScriptableObject
{
    public int id;
    public float speed;
    public int health;
    public int damage;
    public int minCost;
    public int maxCost;
    public GameObject enemyPrefab;
}
