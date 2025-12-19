using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   //estos datos guardan la posicion y rotacion de escena en escena 
   public Vector3 savedPosition;
   public Vector3 savedRotation;
   public float savedScore = 0;
   public static GameManager instance;
   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
   }

   public void LoadNewLevel(int levelIndex, Vector3 spawnPosition, Vector3 spawnRotation)
   {
      //guardo la posicion y la rotacion para el nuevo player
      savedPosition = spawnPosition;
      savedRotation = spawnRotation;
      SceneManager.LoadScene(1);
   }
}
