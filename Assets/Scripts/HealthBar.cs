using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
 
    //public Player caractere;        //Receber� o objeto do Player
    public Image medidorImagem;     //Recebe a barra de medi��o de vida
    int maxPontosDano;            //Armazena a quantidade limite de sa�de do Player

    private static int vidaMaxima;
    public Player Player;
    // Start is called before the first frame update
    void Start()
    {
        maxPontosDano = Player.vidaMaxima; //quantidade de vida será o valor MaxPontos dano em caractere
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(Player != null )
        {
            //print($"{maxPontosDano} de vida maxima; {Player.health} de vida atual ");
            //print(Player.health/maxPontosDano);
            medidorImagem.fillAmount = Player.health / maxPontosDano;    //Rela��o percentual entre os pontos de dano e o m�ximo de pontos de dano, alterando a barra de vida vista pelo Player
        }
    }
}
