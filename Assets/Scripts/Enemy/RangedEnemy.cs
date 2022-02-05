using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float stopDistance;
    private float attackTime;
    private Animator animator;
    public Transform shotPoint;
    public GameObject enemyBullet;
    public override void Start()
    {
        base.Start();
        animator=GetComponent<Animator>();
    }
    private void Update() {
        if(player!=null)
        {
            if(Vector2.Distance(transform.position,player.position)>stopDistance)
            {
                transform.position=Vector2.MoveTowards(transform.position,player.transform.position,speed*Time.deltaTime);
            }
            if(Time.time>=attackTime)
            {
                attackTime=Time.time+timeBetweenAttack;
                animator.SetTrigger("attack");
            }
        }
    }
    public void RangedAttack()
    {
        Vector2 direction=player.position-shotPoint.position;
        float angle= Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle-90,Vector3.forward);

        Instantiate(enemyBullet,shotPoint.position,shotPoint.rotation);
    }
}
