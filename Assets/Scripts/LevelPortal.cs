using Unity.Cinemachine;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] private Vector3 spawnRotation;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private int targetScene;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.LoadNewLevel(targetScene, spawnPosition, spawnRotation);
        }
    }
}
