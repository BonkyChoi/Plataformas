using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Image RellenoDeVida;
    private Player player;
    private float HPmax;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        HPmax = player.maxHP;
    }
    
    void Update()
    {
        RellenoDeVida.fillAmount = player.currentHP / HPmax;
    }
}
