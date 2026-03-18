using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float xMin = -8.5f;
    [SerializeField] private float xMax = 8.5f;
    [SerializeField] private float y = 7f;

    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private List<GameObject> items;


    // Y = 7.00
    // X -8.5, 8.5

    // Her spawnInterval de (1 sn de ) bir kez rastgele bir Item rastgele bir x pozisyonunda spawn Edicez

    private float spawnTimer = 0;

    private void Update()
    {
        // Geçen zamanı toplayarak saymış oluyoruz
        spawnTimer = spawnTimer + Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            // Spawn et

            SpawnItem();

            spawnTimer = 0;
        }
    }

    private void SpawnItem()
    {
        // Spawn mekaniğini yaz

        float randomXPosition = Random.Range(xMin, xMax);
        Vector3 randomPos = new Vector3(randomXPosition, y, 0f);

        int randomIndex = Random.Range(0, items.Count);
        GameObject randomItem = items[randomIndex];
        
        // Örnekle
      GameObject instantiatedItem =  Instantiate(randomItem, randomPos, Quaternion.identity);
      instantiatedItem.transform.SetParent(transform);
      
    }
}