using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 1.0f;
    public float movementRadius = 5.0f; // radio movimiento
    public float delayBetweenMovements = 5.0f; // Delay

    private Vector3 originalPosition;
    private Vector3 randomDirection;
    private float TimerEntreMovements = 0.0f;

    public float detectionRadius = 5.0f;
    private bool playerNear = false;

    public float jumpHeight = 0.5f;
    public float jumpFrequency = 1.0f; // How many jumps per second
    private Rigidbody rb;


    private void Start()
    {
        originalPosition = transform.position;
        randomDirection = GetRandomPositionInRadius();
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null)
            {
                playerTransform = playerObject.transform;
            }
        }

        TimerEntreMovements += Time.deltaTime;

        // Delay del movimiento
        if (TimerEntreMovements >= delayBetweenMovements && !PlayerVisible())
        {
            MoveToRandomPosition();
            TimerEntreMovements = 0.0f;
        }


    }

    private void FixedUpdate()
    {
        if (PlayerVisible())
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            transform.Translate(directionToPlayer * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            Debug.DrawLine(transform.position, playerTransform.position, Color.green);
            Vector3 directionToDestination = (randomDirection - transform.position).normalized;
            transform.Translate(directionToDestination * moveSpeed * Time.fixedDeltaTime);
        }
        GameObject jumpCube = GameObject.FindGameObjectWithTag("Jumper");
        float jumpCubeDistance = Vector3.Distance(transform.position, jumpCube.transform.position);
        if (jumpCubeDistance <= detectionRadius)
        {
            if (!playerNear)
            {
                playerNear = true;
                Debug.Log("CILINDRO CERCA CUBO");
            
                Vector3 randomSpawnPos = new Vector3(Random.Range(-30,30),1,Random.Range(-30,30));
                Instantiate(jumpCube, randomSpawnPos, Quaternion.identity);
                Destroy(jumpCube);
            }
        }
        else{
            playerNear=false;
        }

    }
//FUNCTIONS!!! :D
    private bool PlayerVisible()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, playerTransform.position, out hit))
        {
            Debug.DrawLine(transform.position, playerTransform.position, Color.red);
            return hit.collider.CompareTag("Player");
        }
        return false;
    }


    private void MoveToRandomPosition()
    {
        randomDirection = GetRandomPositionInRadius();
    }

    private Vector3 GetRandomPositionInRadius()
    {
        return originalPosition + Random.insideUnitSphere * movementRadius;
    }

}