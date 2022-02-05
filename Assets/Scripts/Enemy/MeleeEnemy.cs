using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float stopDistance;
    private float attackTime;
    public float attackSpeed;
    // Update is called once per frame
    void Update()
    {
        if(player!=null)
        {
            if(Vector2.Distance(transform.position,player.transform.position)>stopDistance)
            {
                transform.position=Vector2.MoveTowards(transform.position,player.transform.position,speed*Time.deltaTime);
            }
            else
            {
                if(Time.time>=attackTime)
                {
                    StartCoroutine(Attack());
                    attackTime=Time.time+timeBetweenAttack;
                }
            }
        }
    }
    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeADamage(damage);
        Vector2 originalPos= transform.position;
        Vector2 targetPos=player.position;

        float percent=0;
        while(percent<=1)
        {
            percent+=Time.deltaTime;
            float formula= (-Mathf.Pow(percent,2)+percent*4);
            transform.position=Vector2.Lerp(originalPos,targetPos,formula);
            yield return null;
        }
    }
}
