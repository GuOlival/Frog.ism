using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public int health;
    public int dano;
    Coroutine danoCoroutine;
    Animator aimAnimator;
    public AudioSource tomouDano;
    public SpriteRenderer spriteRenderer;
    public Sprite morto;
    public AudioSource morreu;
    public bool rei;
    // Start is called before the first frame update
    void Start()
    {
        aimAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(int damage)
    {
        StartCoroutine(EfeitoFlicker());
        tomouDano.Play();
        health -= damage;
        Debug.Log("Toma Hit");
        Debug.Log(health);
    }
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
          morreInimigo();
        }
        
        /*if(podeAtirar == false){ //verifica se pode atacar, se não continua a contar no contador
            reload -= Time.deltaTime;
        }   
        if(reload < 0){ //se o contador terminou
            podeAtirar =true; //permite atirar
            reload = oldReload; //e armazena o valor de reload novamente
        }*/
    }

    public void morreInimigo()
    {
        morreu.Play();
        Destroy(gameObject);
    }

    public IEnumerator EfeitoFlicker()
    {
       GetComponent<SpriteRenderer>().color = Color.red; //pega a cor da sprite e muda pra vermelho
       yield return new WaitForSeconds(0.1f);          //espera 0.1 segundos
       GetComponent<SpriteRenderer>().color = Color.white;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Bateu");
            Player player = col.gameObject.GetComponent<Player>();
            if(danoCoroutine == null)
            {
                if(!rei){
                    aimAnimator.SetBool("Ataca",true);  
                }
                danoCoroutine = StartCoroutine(player.TomaDano(dano, 1.0f));
            }
        }
    }

    void ParaAtaque()
    {
        Debug.Log("Para ataca");
        if(!rei){
            aimAnimator.SetBool("Ataca",false);
        }
    }

    void OnCollisionExit2D(Collision2D col) //quando a colisao sair
{
    if(col.gameObject.CompareTag("Player")) //se for o player saindo
    {
        if (danoCoroutine != null) //e ainda estiver dando dano
        {
            StopCoroutine(danoCoroutine); //para de executar a funcao que da dano
            danoCoroutine = null; //torna nulo
        }
    }
}

   
}
