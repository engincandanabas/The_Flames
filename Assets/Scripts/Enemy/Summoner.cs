using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float minX, minY, maxX, maxY;
    private Vector2 targetPosition;
    private Animator anim;

    public float timeBetweenSummons,attackSpeed,stopDistance;
    private float summonTime,timer;
    public Enemy enemyToSumon;
    public override void Start()
    {
        base.Start();
        float randomX=Random.Range(minX,maxX);
        float randomY=Random.Range(minY,maxY);
        targetPosition=new Vector2(randomX,randomY);
        anim = GetComponent<Animator>();

    }
    private void FixedUpdate() {
        if(player!=null)
        {
            if(Vector2.Distance(transform.position,targetPosition)>.5f)
            {
                transform.position=Vector2.MoveTowards(transform.position,targetPosition,speed*Time.deltaTime);
                anim.SetBool("isRunning",true);
            }
            else
            {
                anim.SetBool("isRunning",false);
                if(Time.time>=summonTime)
                {
                    summonTime=Time.time+timeBetweenSummons;
                    anim.SetTrigger("summon");

                }
            }
            if(Vector2.Distance(transform.position,player.transform.position)<stopDistance)
            {
                if(Time.time>timer)
                {
                    timer=Time.time+timeBetweenAttack;
                    StartCoroutine(Attack());   
                }
            }
        }
    }
    public void Summon()
    {
        if(player!=null)
        {
            Instantiate(enemyToSumon,transform.position,transform.rotation);
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


