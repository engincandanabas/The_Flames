using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectTile;
    public Transform shotPoint;
    public float timeBetweenShots;
    private float shotTime;
    Animator animator;
    void Start()
    {
        animator=Camera.main.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction=Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        float angle= Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle-90,Vector3.forward);
        transform.rotation=rotation;

        if(Input.GetMouseButtonDown(0))
        {
            if(Time.time>=shotTime)
            {
                Instantiate(projectTile,shotPoint.position,transform.rotation);
                animator.SetTrigger("shake");
                shotTime=Time.time+timeBetweenShots;
            }
        }
    }
}
