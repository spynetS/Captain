// enemyData
using UnityEngine;


    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyData", order = 1)]

public class EnemyData : ScriptableObject
{
    public int hp;      //health of the enemy
    public int damage;  //damage of the enemy
    public float speed; //speed of the enemy
}