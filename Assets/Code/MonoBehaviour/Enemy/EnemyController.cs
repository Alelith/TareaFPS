using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Data")]
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private int blinkTimes;

    private float currentLife;

    protected SpriteRenderer spriteRenderer;

    private bool isHurt;

    private void Awake()
    {
        currentLife = enemyData.MaxLife;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().DamagePlayer(enemyData.Damage);
        }
    }

    public void DamageEnemy(float quantity)
    {
        if (!isHurt)
        {
            currentLife -= quantity;
            if (currentLife <= 0) 
                Destroy(gameObject);
            StartCoroutine(HurtAnimation());
            GUIController.instance.UpdateScore(enemyData.EnemyScorePoint);
        }
    }

    private IEnumerator HurtAnimation()
    {
        var blinkTimesTemp = blinkTimes;
        isHurt = true;
        do
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            blinkTimes--;
        } while (blinkTimes > 0);

        isHurt = false;
        blinkTimes = blinkTimesTemp;
    }

    public EnemyData EnemyData { get => enemyData; set => enemyData = value; }
}
