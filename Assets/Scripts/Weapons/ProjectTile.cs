using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    public float speed,lifeTime;
    public int damage;
    public GameObject explosion,soundobject;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(soundobject,transform.position,transform.rotation);
        Invoke("DestroyProjectTile",lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up*speed*Time.deltaTime);
    }
    void DestroyProjectTile()
    {
        Instantiate(explosion,transform.position,Quaternion.identity);
        Destroy(gameObject); 
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeADamage(damage);
            DestroyProjectTile();
        }
        if(other.gameObject.CompareTag("boss"))
        {
            other.GetComponent<Boss>().TakeADamage(damage);
            DestroyProjectTile();
        }
    }
}
