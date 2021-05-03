using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inimigo;
    public Transform[] posicoes;    
    void Start()
    {
        
    }

    void SpawnO(Transform posicao)
    {
        Instantiate(inimigo, posicao.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            foreach(Transform pos in posicoes)
            {
                SpawnO(pos);
                
            }
            Destroy(gameObject, 1);
        }   
        //Destroy(gameObject);
    }
}
