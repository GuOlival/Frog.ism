using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class Dialog : MonoBehaviour
{
    //public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    public Text textDisplay;
    public Image fundoDialogo;
    public Image fundoBotao;
    public Text mudaObjetivo;
    public Tilemap tile;
    void Start()
    {
        //StartCoroutine(Type());

    }

    void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            fundoBotao.enabled = true;
            continueButton.SetActive(true);
        }
    }
    public IEnumerator Type()
    {
        Time.timeScale = 0f;
        fundoDialogo.enabled = true;
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        
    }
    int cena=0;

    public void NextSetence()
    {
        fundoBotao.enabled = false;
        continueButton.SetActive(false);
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            Time.timeScale = 1f;
            fundoDialogo.enabled = false;
            if(cena == 0){
                mudaObjetivo.text = "Objective: Find the Red Pig";
                tile.ClearAllTiles();
                cena = 1;
            }
            else if(cena == 1)
            {
                mudaObjetivo.text = "Objective: Kill the King Frog";
                cena = 2;
            }
        }
    }
}
