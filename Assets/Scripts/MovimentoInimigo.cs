using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Poe como requisito ter um
RigidBody2d, um CircleCollider2d
e um componente de animação*/
[RequireComponent(typeof(Rigidbody2D))] 
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]


public class MovimentoInimigo : MonoBehaviour
{
    public bool Rei;
    public float velocidadePerseguicao; //velocidade do inimigo perseguindo o player
    public float velocidadePerambular; //velocidade do inimigo andando normal
    float velocidadeCorrente; //velocidade do inimigo atribuida 

    public float intervaloMudancaDirecao; //tempo para alterar a direção
    public bool perseguePlayer;         //indicador de perseguidor ou não
    Coroutine moverCoroutine;

    Rigidbody2D rb2D;   //terá componente rigidbody
    Animator animator;  //armazena componente animator

    Transform alvoTransform = null; //armazena a posicao do alvo
    Vector3 posicaoFinal;
    float anguloAtual = 0;  //angulo atribuido
    
    CircleCollider2D circleCollider; //armazena o componente de Spot
    public GameObject cuspe;
    public AudioSource somCuspe;
    private List<Rigidbody2D> EnemyRBs;
    private Rigidbody2D rb;
    public float reload;
    bool podeAtirar=true;
    float oldReload;
    private float repelRange = .5f;
    public Sprite morto;
    SpriteRenderer spriteRenderer;
    public float speed;
    private Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        oldReload = reload;
        animator = GetComponent<Animator>(); //armazena o componente de animação
        velocidadeCorrente = velocidadePerambular; //armazena a velocidade de andar
        rb2D = GetComponent<Rigidbody2D>(); //armazena a componente do rigidbody
        StartCoroutine(rotinaPerambular());     //inicia a coroutine de começar a perambular
        circleCollider = GetComponent<CircleCollider2D>(); //pega a componente circlecollider
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        EnemyRBs.Remove(rb);
    }


    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        if (EnemyRBs == null)
        {
            EnemyRBs = new List<Rigidbody2D>();
        }

        EnemyRBs.Add(rb);
    }

    void FixedUpdate()
    {
        Vector2 repelForce = Vector2.zero;
        foreach (Rigidbody2D enemy in EnemyRBs)
        {
            if (enemy == rb)
            {
                continue;
            }
            if (Vector2.Distance(enemy.position, rb.position) <= repelRange)
            {
                Vector2 repelDir = (rb.position - enemy.position).normalized;
                repelForce += repelDir;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position,circleCollider.radius); //desenha a reta para indicar a posicao
        }
    }

    public IEnumerator rotinaPerambular()
    {
        while(true)
        {
            EscolheNovoPontoFinal(); //escolhe novo ponto final
            if(moverCoroutine != null) //se ele estiver se movendo
            {
                StopCoroutine(moverCoroutine); //para de se mover
            }
            moverCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente)); //começa a se mover executando a coroutine de mover
            yield return new WaitForSeconds(intervaloMudancaDirecao); //espera o intervalo de mudanca de direcao para poder retornar
        }
    }
    public IEnumerator Mover(Rigidbody2D rbParaMover, float velocidade)
    {
        float distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude; //armazena a distancia faltando
        while(distanciaFaltante > float.Epsilon) //se for maior que zero
        {
            if(alvoTransform != null) //se houver alvo transform
            {
                posicaoFinal = alvoTransform.position; //posicao final será a posicao do alvo
            }
            if(rbParaMover != null) //se o rigidbody é diferente de nulo
            {
                animator.SetBool("Caminhando", true); //indica que ele esta caminhando
                Vector3 novaPosicao = Vector3.MoveTowards(rbParaMover.position, posicaoFinal, velocidade*Time.deltaTime); //indica onde sera nova posicao
                rb2D.MovePosition(novaPosicao); //se move ate a nova posicao
                distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude; //e adquire a distancia faltante
            }
            yield return new WaitForFixedUpdate(); //espera pelo fixed update
        }
        animator.SetBool("Caminhando",false); //troca que não esta mais caminhando
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player") && perseguePlayer) //se o trigger ativo for o player e for um inimigo que persegue
        {
            velocidadeCorrente = velocidadePerseguicao; //velocidade do inimigo muda para velocidade de perseguicao
            alvoTransform = col.gameObject.transform; //pega o transform do player e armazena
            if(moverCoroutine != null) //se estiver se movendo
            {
                StopCoroutine(moverCoroutine); //para de perambular
            }
            moverCoroutine = StartCoroutine(Mover(rb2D,velocidadeCorrente)); //e começa a se mover para o player
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if(Rei && col.gameObject.CompareTag("Player"))
        {
            Shoot();
        }
    }

    void  Shoot()
    {
        if(podeAtirar){
            somCuspe.Play();
            Instantiate(cuspe, transform.position, Quaternion.identity);
            podeAtirar = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")) //se o trigger que saiu da area de colisao for a do player
        {
            animator.SetBool("Caminhando", false); //para com animacao de caminhar
            velocidadeCorrente = velocidadePerambular; //velocidade do inimigo vira a velocidade de perambular
            if(moverCoroutine != null) //se estiver se movendo
            {
                StopCoroutine(moverCoroutine); //para de se mover
            }
            alvoTransform = null; //não tem mais alvo
        }
    }


    void EscolheNovoPontoFinal()
    {
        anguloAtual += Random.Range(0,360); //escolhe um angulo aleatorio
        anguloAtual = Mathf.Repeat(anguloAtual, 360); //pega o modulo desse angulo
        posicaoFinal += Vector3ParaAngulo(anguloAtual); //pega a posicao final com base no vetor transformado em angulo
    }

    Vector3 Vector3ParaAngulo(float anguloEntrada)
    {
        float anguloEntradaRad = anguloEntrada * Mathf.Deg2Rad; //pega o angulo de entrada em rad transformando o de graus
        return new Vector3(Mathf.Cos(anguloEntradaRad), Mathf.Sin(anguloEntradaRad)); //e retorna o vetor com base no cos e no sen

    }
    // Update is called once per frame
    void Update()
    {
        if(podeAtirar == false){ //verifica se pode atacar, se não continua a contar no contador
            reload -= Time.deltaTime;
        }   
        if(reload < 0){ //se o contador terminou
            podeAtirar =true; //permite atirar
            reload = oldReload; //e armazena o valor de reload novamente
        }
        Debug.DrawLine(rb2D.position, posicaoFinal, Color.red); //debuga desenhando uma linha indicando posicao final
        if (Vector2.Distance(transform.position, playerPos.position) > 0.25f)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, velocidadeCorrente * Time.deltaTime);
        }
    }
}
