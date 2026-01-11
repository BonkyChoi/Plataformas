using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   //estos datos guardan la posicion y rotacion de escena en escena 
   public Vector3 SavedPosition { get; set; }
   public Vector3 SavedRotation { get; set; }
   public float SavedScore { get; } = 0;
   public static GameManager instance;
   public GameObject gameOverPanel;
   private bool GameOverActivo;

   void Start()
   {
      if (gameOverPanel != null)
      {
         gameOverPanel.SetActive(false);
      }
   }
   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else if (instance != this)
      {
         Destroy(gameObject);
      }
   }

   private void Update()
   {
      if (GameOverActivo)
      {
         if (Input.GetKeyDown(KeyCode.R))
         {
            ReiniciarEscena();
         }

         if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
         {
            IrAlMenu();
         }
      }
   }

   public void LoadNewLevel(int levelIndex, Vector3 spawnPosition, Vector3 spawnRotation)
   {
      //guarda la posicion y la rotacion
      GameOverActivo = false;
      SavedPosition = spawnPosition;
      SavedRotation = spawnRotation;
      SceneManager.LoadScene(levelIndex);
   }
   public void GameOver()
   {
     
      GameOverActivo = true;
      if (gameOverPanel != null)
      {
         gameOverPanel.SetActive(true);
      }


   }

   public void ReiniciarEscena()
   {
      GameOverActivo = false;
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

   public void IrAlMenu()
   {
      SceneManager.LoadScene("MainMenu");
   }
   
}
