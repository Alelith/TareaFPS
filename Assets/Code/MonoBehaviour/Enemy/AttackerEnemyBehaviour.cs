using UnityEngine;

public class AttackerEnemyBehaviour : BaseEnemyBehaviour
{
    [SerializeField]
    private float attackTimer;

    private float attackCounter = 0;
    private EnemyWeaponController enemyWeapon;
    private Animator animator;

    private void Awake()
    {
        enemyWeapon = GetComponent<EnemyWeaponController>();

        animator = GetComponent<Animator>();
    }

    #region Attack Functions
    private void OnAttackEnter()
    {
        agent.ResetPath();
        //animator.Play("Attack");
    }

    private void Attack()
    {
        attackCounter += Time.deltaTime;
        if (!withinAttackRange)
            brain.PopState();
        else if (attackCounter >= attackTimer)
        {
            attackCounter = 0;
            enemyWeapon.Attack(player.transform);
        }
    }

    private void OnAttackExit()
    {
        animator.Play("Idle");
    }
    #endregion

    protected override void Chase()
    {
        agent.SetDestination(player.transform.position);
        if (!playerIsNear)
            brain.PopState();
        if (withinAttackRange)
            brain.PushState(Attack, OnAttackEnter, OnAttackExit);
    }
}
