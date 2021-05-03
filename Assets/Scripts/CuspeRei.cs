using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuspeRei : MonoBehaviour
{
    public float speed = 20f; //velocidade da blaa
    public int danoBala; //indica o valor do dano da bala
    Coroutine danoCoroutine;
    Rigidbody2D rb ;
    //public bool Atirador;
    Player target; //target pega player
    void Start()
    {   
        rb =  GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>(); //armazena o gmaeobject player
        Vector2 moveDirection = (target.transform.position - transform.position); //direção de movimento é igual a do player 
        Vector2 aimDirection = (moveDirection).normalized;                        //menos a a posição de onde a bala foi instanciada
        rb.velocity = aimDirection * speed;                                     //põe velocidade na bala
    }
    
    private void OnCollisionEnter2D(Collision2D col){ //se entrar na área
        if(col.gameObject.CompareTag("Player")) //se essa área for a do player
        {
            
                danoCoroutine = StartCoroutine(target.TomaDano(danoBala, 1.0f)); //executa a coroutine e da dano no player com dano da bala
                Destroy(gameObject); //destroi a bala
        }
    }

    void Update()
    {
        Destroy(gameObject, 4);
    }
}
