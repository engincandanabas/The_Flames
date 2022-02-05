using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health,damage;
    public Enemy[] enemies;
    public float spawnOffset;

    private int halftHealth;
    public GameObject bloodEffect;
    private Animator animator;
    private Slider healthSlider;
    private SceneTransition sceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        halftHealth=health/2;
        animator=GetComponent<Animator>();
        healthSlider=FindObjectOfType<Slider>();
        healthSlider.maxValue=health;
        healthSlider.value=health;
        sceneTransition=FindObjectOfType<SceneTransition>();
    }

    // Update is called once per frame
    public void TakeADamage(int amount)
    {
        health -=amount;
        healthSlider.value=health;
        if(health<=0)
        {
            healthSlider.gameObject.SetActive(false);
            Instantiate(bloodEffect,transform.position,transform.rotation);
            Destroy(gameObject);
            sceneTransition.LoadScene("Win");
        }
        if(health<=halftHealth)
        {
            animator.SetTrigger("stage2");
        }
        Enemy randomEnemy=enemies[Random.Range(0,enemies.Length)];
        Instantiate(randomEnemy,transform.position+new Vector3(spawnOffset,spawnOffset,0),transform.rotation);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeADamage(damage);
        }
    }
}
