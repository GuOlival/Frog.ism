using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    public float VelocidadeMovimento = 3.0f; //equivale ao momento
    Vector2 Movimento = new Vector2(); //detectar movimento pelo teclado

    Animator animator;      //guarda a componente do Controlede animação
    //string estadoAnimacao = "EstadoAnimacao"; //guarda o nome do parametro de animação
    Rigidbody2D rb2D; //guarda componente corpo rigido do player
    
    enum EstadosChar
    {

    }
    Player Player;
    void Start()
    {
        animator = GetComponent<Animator>(); //pega componente animator
        rb2D = GetComponent<Rigidbody2D>(); //pega o rigidbody
        Player = GetComponent<Player>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Player.playerMorto == false)
        {
            UpdateEstado(); //atualiza estado
        }
    }

    private void FixedUpdate()
    {
        if(Player.playerMorto == false)
        {
            MoveCaractere(); //move caractere
        }
    }

    private void MoveCaractere()
    {
        Movimento.x = Input.GetAxisRaw("Horizontal"); //pega o movimento de x a partir do input
        Movimento.y = Input.GetAxisRaw("Vertical"); //pega o movimento de y a partir do input
        Movimento.Normalize(); //normaliza esse vetor
        rb2D.velocity = Movimento * VelocidadeMovimento; //e faz com que o rigidbody se mecha com base no vetor e da velocidade de movimento
    }

    private Vector2 lastMoveDir; 
    void UpdateEstado()
    {
        bool isIdle = Movimento.x == 0 && Movimento.y == 0;
        if(isIdle)
        {
            animator.SetBool("isMoving",false);
        } 
        else
        {
            lastMoveDir = Movimento;
            animator.SetFloat("DirX", Movimento.x); //seta o float da animacao em x
            animator.SetFloat("DirY", Movimento.y);  //seta o float da anima
            animator.SetBool("isMoving", true); //esta caminhando
        }
        
    }
}
