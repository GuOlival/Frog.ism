using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Boss : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite fase1, fase2, fase3;
    Inimigo inimigo;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        inimigo = GetComponent<Inimigo>();
    }

    void Fase2()
    {
        spriteRenderer.sprite = fase2;
    }

    void Fase3()
    {
        spriteRenderer.sprite = fase3;
    }
    // Update is called once per frame
    void Update()
    {
        if(inimigo.health<=25 && inimigo.health>10)
        {
            Fase2();
        }   
        else if (inimigo.health <= 10)
        {
            Fase3();
        }
        if(inimigo.health<=0)
        {
            SceneManager.LoadScene("Chefe");
        }
    }
}
