using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
     public Dialog dialog;
    //public Dialog dialogo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        Debug.Log("Colidiu");
        if(col.gameObject.CompareTag("Player"))
        {
            //executa a ação de aparecer a caixa de texto
            StartCoroutine(dialog.Type());
        }
    }
}
