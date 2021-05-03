using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextoMenu : MonoBehaviour
{
    public string sentence;
    private int index;
    public float typingSpeed;
    public Text textDisplay;
    public GameObject botaoJogar;
    void Start()
    {
        StartCoroutine(Type());
    }

    public IEnumerator Type()
    {
        foreach(char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    void Update()
    {
        if(textDisplay.text == sentence)
        {
            botaoJogar.SetActive(true);
        }
    }

    public void NextSetence()
    {
        /*if(index < sentences.Length - 1)
        {
            textDisplay.SetAllDirty();
            index++;
            //textDisplay.text += "\n";
            StartCoroutine(Type());
        }*/
    }
}
