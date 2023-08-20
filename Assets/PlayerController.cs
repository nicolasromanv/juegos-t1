using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //Variables or something like it
    public Rigidbody rb;
    public float forwardForce = 200f;
    public float sidewaysForce =200f;
    public int maxHealth =100;
    public int currenthealth;
    public HealthBar healthBar;
    public float detectionRadius = 5.0f;
    private bool playerNear = false;
    private bool playerNear2 = false;
    private float jumpCubeDistance;
    // Start is called before the first frame update
    
    void Start() // cuando inicia el juego, se activa esto
    {
        //rb.AddForce(0,200,500);
        currenthealth=maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Se llama antes de Update al mismo ratio basado en el Delta Time, recomendable colocar las físicas aquí
    void FixedUpdate()
    {


    }

    // Update is called once per frame // Llamado una vez por frame
    void Update()
    {

        if(Input.GetKey("w")){
            rb.AddForce(0,0,forwardForce*Time.deltaTime, ForceMode.VelocityChange);
            
        }
        if(Input.GetKey("s")){
            rb.AddForce(0,0,-forwardForce*Time.deltaTime, ForceMode.VelocityChange);
        }
        if(Input.GetKey("a")){
            rb.AddForce(-sidewaysForce*Time.deltaTime,0,0, ForceMode.VelocityChange);
        }
        if(Input.GetKey("d")){
            rb.AddForce(sidewaysForce*Time.deltaTime,0,0, ForceMode.VelocityChange);
        }
        
        GameObject jumpCube = GameObject.FindGameObjectWithTag("Jumper");
        float jumpCubeDistance = Vector3.Distance(transform.position, jumpCube.transform.position);



        GameObject DamagerCyl = GameObject.FindGameObjectWithTag("Damager");
        float DamagerCylDistance = Vector3.Distance(transform.position, DamagerCyl.transform.position);


    /*ENEMY CUBE*/
        if (jumpCubeDistance <= detectionRadius)
        {
            if (!playerNear)
            {
                playerNear = true;
                Debug.Log("+10 de HP");
                TakeDamage(-10);
            }
        }
        else{
            playerNear=false;
        }

    /*ENEMY Cylinder*/
        if (DamagerCylDistance <= detectionRadius)
        {
            if (!playerNear2)
            {
                playerNear2 = true;
                Debug.Log("you've been damaged!");
                TakeDamage(20);
            }
        }
        else{
            playerNear2=false;
        }

    }

    void TakeDamage(int damage){
        currenthealth-=damage;
        if (currenthealth>=maxHealth){
            currenthealth=maxHealth;
        }
        else{
        healthBar.SetHealth(currenthealth);
        }
    }

    //Llamado después de cada Update
    void LateUpdate(){

    }
}
