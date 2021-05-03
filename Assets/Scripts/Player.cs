using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    //public int vidaMaxima;
    public  float health;
    public static int vidaMaxima = 10;
    public HealthBar healthBarPrefab;   //Refer�ncia ao objeto prefab criado da HealthBar
    Animator aimAnimator;
    public  bool encontrouChefe;
    HealthBar healthBar;
    public AudioSource somMorte;
    // Start is called before the first frame update
    void Start()
    {
        health = vidaMaxima;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.Player = this;
        aimAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        

       //Debug.Log(encontrouChefe);
    }

    void PlayerMorre(bool fim)
    {
        Debug.Log("Morreu");
        if(fim == true)
        {
            SceneManager.LoadScene("Gameover");
        }
        else
        {
            SceneManager.LoadScene("Gameover");
        }
    }

    void terminaMorte()
    {
        PlayerMorre(encontrouChefe);
        Destroy(gameObject);
      
    }

    public IEnumerator TomaDano(int dano, float intervalo)
    {
         while(true){
            StartCoroutine(EfeitoFlicker()); //executa o efeito de flicker
            health -= dano; //tira os pontos de vida
            if(health <= float.Epsilon){ //se for menor que zero
                StartCoroutine(KillCaractere());
                break;
            }
            if(intervalo > float.Epsilon){ //se for maior que zero
                yield return new WaitForSeconds(intervalo); //espera o intervalo para sofrer dano
            }
            else{
                break;
            }
         }
    }

    public IEnumerator EfeitoFlicker()
    {
       GetComponent<SpriteRenderer>().color = Color.red; //pega a cor da sprite e muda pra vermelho
      
       yield return new WaitForSeconds(0.4f);          //espera 0.1 segundos
       GetComponent<SpriteRenderer>().color = Color.white;
       
    }


    public bool playerMorto;
    public  IEnumerator KillCaractere() //mata personagem
    {       
        //podeMovimentar = false; //não pode mais se movimentar
        //aimAnimator = GetComponent<Animator>(); //pega componente de animação
        //somMorte.Play(0); //toca som da morte
        playerMorto = true;
        aimAnimator.speed = 0;
        aimAnimator.SetBool("morre", true);
        somMorte.Play();
        yield return new WaitForSeconds(0.5f);
        PlayerMorre(encontrouChefe);
        Destroy(gameObject); //muda a animação do player para a de morte
        //base.KillCaractere();           //Mata o personagem
          //Destr�i a Health Bar
    }
    void GameOver(){ //função executa quando acaba animação de morte do player
        //base.KillCaractere();
        Destroy(healthBar.gameObject); //destroi a healthbar
        //podeMovimentar = true; //pode se movimentar novamente, já que caso o jogador queira tentar mais uma vez possa se movimentar
        //SceneManager.LoadScene("GameOver_Scene");//Vai para a tela de Fim de Jogo
        
    }






    private void OnTriggerEnter2D (Collider2D collision) //entrando em uma área
    {
        //Se o Game Object que colidiu for um Coletavel
        if (collision.gameObject.CompareTag("Coletavel"))
        {
                AjustePontosDano(20);
                collision.gameObject.SetActive(false);  //Desativa o GameObject que coletou da tela
        }
    }
    
    
    public void AjustePontosDano(int quantidade) //função para ajustar pontos de dano
    {
        health += quantidade;
        if(health > vidaMaxima)
        {
            health = vidaMaxima;
        }
    }


}
