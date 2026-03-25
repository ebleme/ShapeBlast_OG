using System;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask playerLayer;
    
    
    private bool isHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        isHit = Physics.Raycast(player.position, player.right, out RaycastHit hit, 100, enemyLayer);
        if (isHit)
        {
            Debug.Log("Raycast Hit: " + hit.transform.name);
        }
    }

    private void OnDrawGizmos()
    {
        if (!isHit)
            Gizmos.color = Color.purple;
        else
            Gizmos.color = Color.green;
        Gizmos.DrawLine(player.position, player.right * 100);
    }
}