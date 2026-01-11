using System;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
   [SerializeField] private float downSpeed = 20f;

   private void OnTriggerStay2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         transform.position += Vector3.down * downSpeed * Time.deltaTime;
      }
   }
}
