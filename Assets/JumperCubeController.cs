using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    public Transform playerCube;
    public float detectionRadius = 5.0f;
    public float jumpHeight = 0.5f;
    public float jumpFrequency = 1.0f; // How many jumps per second

    private Rigidbody rb;
    private bool playerNear = false;

    private float jumpTimer = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerCube.position);


        /*COMPORTAMIENTO CON CUBO-PLAYER*/
        if (distance <= detectionRadius)
        {
            if (!playerNear)
            {
                playerNear = true;
                //Debug.Log("near!!!");
                rb.velocity = Vector3.zero; // Stop any existing motion
            }
        }
        else
        {
            if (playerNear)
            {
                playerNear = false;
                //Debug.Log(" NO near!!!");
            }

            // Idle behavior (jumping up and down)
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= 1.0f / jumpFrequency)
            {
                Jump();
                jumpTimer = 0.0f;
            }
        }

        /*COMPORTAMIENTO CON Cilindro*/
        //GameObject DamagerCyl = GameObject.FindGameObjectWithTag("Damager");
        //float DamagerCylDistance = Vector3.Distance(transform.position, DamagerCyl.transform.position);

        



    }

    private void Jump()
    {
        Vector3 jumpForce = Vector3.up * Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
        rb.AddForce(jumpForce, ForceMode.VelocityChange);
    }
}
