using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class PlayerMira : MonoBehaviour
{
    private Animator aimAnimator;
    float angle;
    public float reload;
    float oldReload;
    bool podeAtacar = true;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    Player Player;
    public AudioSource somHit;

    // Start is called before the first frame update
    private void Awake()
    {
        Player = GetComponent<Player>();
        oldReload = reload;
        aimAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log(podeAtacar);
        if(podeAtacar && Player.playerMorto == false)
        {
            HandleAiming();
            HandleShooting();
        }
        if(podeAtacar == false)
        {
            reload -= Time.deltaTime;
            
        }
        if(reload < 0)
        {
            aimAnimator.SetBool("vaiAtacar",false);
            podeAtacar = true;
            reload = oldReload;
        }
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x)*Mathf.Rad2Deg;
        //Debug.Log(angle);
    }
    private void HandleShooting()
    {
        if(Input.GetMouseButtonDown(0) && (angle>60 && angle <120) && podeAtacar) //ataca cima
        {
            
            aimAnimator.SetBool("vaiAtacar",true);
            aimAnimator.SetFloat("MiraX", 0f);
            aimAnimator.SetFloat("MiraY", 1f);    
            attackPos.position = new Vector3(0f,0.63f,0) + transform.position; 
            //foiHit();
            podeAtacar = false;                           
            
        }
        if(Input.GetMouseButtonDown(0) && (angle>-60 && angle <60) && podeAtacar) //ataca direita
        {
            
            aimAnimator.SetBool("vaiAtacar",true);
            aimAnimator.SetFloat("MiraX", 1f);
            aimAnimator.SetFloat("MiraY", 0f);      
            attackPos.position = new Vector3(0.566f,0f,0f) + transform.position;   
            //foiHit();                       
            podeAtacar = false;      
        }
        if(Input.GetMouseButtonDown(0) && (angle>-120 && angle <-60) && podeAtacar) //ataca baixo
        {
            
            aimAnimator.SetBool("vaiAtacar",true);
            aimAnimator.SetFloat("MiraX", 0f);
            aimAnimator.SetFloat("MiraY", -1f);       
            attackPos.position = new Vector3(0f,-0.63f,0f) + transform.position;        
           // foiHit();                  
            podeAtacar = false;      
        }
        if(Input.GetMouseButtonDown(0) && (angle>120 || angle <-120) && podeAtacar) //ataca esquerda
        {
            
            aimAnimator.SetBool("vaiAtacar",true);
            aimAnimator.SetFloat("MiraX", -1f);
            aimAnimator.SetFloat("MiraY", 0f);      
            attackPos.position = new Vector3(-0.566f,0f,0f) + transform.position;     
            //foiHit();                    
            podeAtacar = false;      
        }
    }

    public void foiHit()
    {
        
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemies);
        if(enemiesToDamage.Length != 0)
        {
            for(int i = 0; i< enemiesToDamage.Length; i++)
            {
                if(enemiesToDamage[i].GetType() == typeof(BoxCollider2D)){
                    enemiesToDamage[i].GetComponent<Inimigo>().TakeDamage(damage);
                }
            }
        }
        //somHit.Play();
    }

    public void SomEspada()
    {
        somHit.Play();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
