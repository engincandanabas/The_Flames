using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health,damage;
    [HideInInspector]
    public Transform player;
    public float speed,timeBetweenAttack;

    public int pickUpChance,healthChance;
    public GameObject[] pickUps;
    public GameObject healthPrefab,enemyDeathParticle,bloodEffect;
    public virtual void Start() {
        player= GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void TakeADamage(int damageAmount)
    {
        health-=damageAmount;
        if(health<=0)
        {
            int randomNumber=Random.Range(0,101);
            if(randomNumber<=pickUpChance)
            {
                GameObject randomPickUp=pickUps[Random.Range(0,pickUps.Length)];
                Instantiate(randomPickUp,transform.position,transform.rotation);
            }
            randomNumber=Random.Range(0,101);
            if(randomNumber<=healthChance)
            {
                Instantiate(healthPrefab,transform.position,transform.rotation);
            }
            Instantiate(enemyDeathParticle,transform.position,transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
