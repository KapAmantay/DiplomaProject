using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    private enum PickUpType
    {
        GoldCoin,
        StaminaGlobe,
        HealthGlobe,
    }

    [SerializeField] private PickUpType pickUpType;
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelerationSpeed = .2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;

    private Vector3 moveDir;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position; // Where is Player
        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance) // Distance check
        {
            moveDir = (playerPos - transform.position).normalized; // Moving towards the Player
            moveSpeed += accelerationSpeed; // Move Speed
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
            DetectPickUpType();
            Destroy(gameObject); // Destroy when it's get to the Player
        }
    }

    private IEnumerator AnimCurveSpawnRoutine() //Popup animation 
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-2f, 2f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animationCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }

    }

    private void DetectPickUpType()
    {
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:
                Debug.Log("Gold");
                break;

            case PickUpType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer();
                Debug.Log("health");
                break;

            case PickUpType.StaminaGlobe:
                Debug.Log("Stamina");
                break;

        }
    } 

}
