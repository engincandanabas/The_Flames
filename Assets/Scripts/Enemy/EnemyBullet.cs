using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Player playerScript;
    private Vector2 targetPosition;
    public float speed;
    public int damage;

    void Start()
    {
        playerScript=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition=playerScript.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,targetPosition)>.1f)
        {
            transform.position=Vector2.MoveTowards(transform.position,targetPosition,speed*Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TakeADamage(damage);
            Destroy(gameObject);
        }
    }
}
