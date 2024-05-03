using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpowner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoinPrefab, healthGlobe, staminaGlobe;

    public void DropItems()
    {
        int randomNum = UnityEngine.Random.Range(1, 5);

        if (randomNum == 1)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }
        if (randomNum == 2)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }
        if (randomNum == 3)
        {
            int randomGold = UnityEngine.Random.Range(1, 4);
            for (int i = 0; i < randomGold; i++) 
            {
                Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
