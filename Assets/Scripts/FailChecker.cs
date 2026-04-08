using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailChecker : MonoBehaviour
{
   [SerializeField] private int maxItemBeforeLose = 10;
   [SerializeField] private Vector2 colliderSize = new Vector2(10, 10);
   
   [SerializeField] private BoxCollider2D collider;

   private void Update()
   {
      var itemsCollided = Physics2D.BoxCastAll(collider.bounds.center, colliderSize, 0,Vector2.zero);

      // if (itemsCollided.Length > 0)
      // {
      //    Debug.Log("Count: " + itemsCollided.Length);
      // }
      
      if (itemsCollided.Length >= maxItemBeforeLose)
      {
         Debug.Log("Lost");
         
         // Sahneyi yenidenbaşlatır
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
      
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(collider.bounds.center, colliderSize);
   }
}
