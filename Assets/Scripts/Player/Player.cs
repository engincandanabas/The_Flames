using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public int health;
    public Image[] hearts;
    public Image cursor;
    public Sprite fullHearts,emptyHearts;
    private Rigidbody2D rb;
    private Animator animator;
    public Animator hurtAnim;
    private Vector2 moveAmount;
    private SceneTransition sceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sceneTransition=FindObjectOfType<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        cursor.gameObject.transform.position=Input.mousePosition;
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }
    public void TakeADamage(int damageAmount)
    {
        health -= damageAmount;
        hurtAnim.SetTrigger("hurt");
        UpdateHealthUI(health);
        if (health <= 0)
        {
            Destroy(this.gameObject);
            sceneTransition.LoadScene("Lose");
        }
    }
    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip,transform.position,transform.rotation,transform);
    }
    void UpdateHealthUI(int currentHealth)
    {
        for(int i=0;i<5;i++)
        {
            if(i<currentHealth)
            {
                hearts[i].sprite=fullHearts;
            }
            else
            {
                hearts[i].sprite=emptyHearts;
            }
        }
    }
    public void Heal(int amount)
    {
        if(health+amount>5)
        {
            health=5;
        }
        else
        {
            health+=amount;
        }
        UpdateHealthUI(health);
    }
}
