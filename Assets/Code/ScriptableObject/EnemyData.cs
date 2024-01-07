using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "FPS/Create new enemy data", order = 1)]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private string enemyName;
    [SerializeField]
    [TextArea]
    private string description;
    [SerializeField]
    private int maxLife;
    [SerializeField]
    private int enemyScorePoint;
    [SerializeField]
    private int damage;

    public int MaxLife { get => maxLife; }
    public int EnemyScorePoint { get => enemyScorePoint; set => enemyScorePoint = value; }
    public int Damage { get => damage; set => damage = value; }
}
