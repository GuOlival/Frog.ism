using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SensorCehfe : MonoBehaviour
{
    public Tilemap add; 
    bool encontrouChefe;
    AudioSource somApareceu;
    
    void Awake()
    {
        somApareceu = GetComponent<AudioSource>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.gameObject.CompareTag("Player")){
            Player player = col.gameObject.GetComponent<Player>();
            player.encontrouChefe = true;
            add.gameObject.SetActive(true);
            somApareceu.Play();
        }
    }
}
