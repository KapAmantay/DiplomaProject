using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelartionRate = .2f;
    [SerializeField] private float moveSpeed = 3f;

    private Vector3 moveDir;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position; // Where is Player
        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance) // Distance check
        {
            moveDir = (playerPos - transform.position).normalized; // Moving towards the Player
            moveSpeed += accelartionRate; // Move Speed
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime; //Move RigBody
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Destroy(gameObject); // Destroy when it's get to the Player
        }
    }
}
