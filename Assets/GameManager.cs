using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text wordText;
    public Text pointsText;
    public GameObject wordImageMenuUI;
    public Image wordImage;
    public int wordLettersRemainingToComplete;
    public string[] animals = new string[] { "C,a,t", "D,o,g", "B,e,e", "A,n,t", "M,o,u,s,e", "H,a,m,s,t,e,r", "R,a,b,b,i,t", "F,i,s,h", "C,r,a,b", "S,h,a,r,k", "D,o,l,p,h,i,n", "F,o,x", "W,o,l,f", "R,h,i,n,o", "K,o,a,l,a", "C,h,e,e,t,a,h", "Z,e,b,r,a", "L,i,o,n", "M,o,n,k,e,y", "G,i,r,a,f,f,e" };
    public string[] numbers = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
    public string[] food = new string[] { "Pasta", "Chips", "Chicken", "Pie", "Cake", "Pizza", "Noodles", "Milk", "Juice", "Eggs", "Meat", "Cheese", "Bread" };
    public string[] fruits = new string[] { "Apple", "Banana", "Pear", "Pineapple", "Grapes", "Kiwi", "Lime", "Cherry", "Peach", "Melon", "Tomato" };
    public string[] colors = new string[] { "Purple", "Pink", "Black", "Green", "Red", "Yellow", "White", "Orange", "Brown", "Grey", "Blue" };
    List<string> currentWordSpplited;
    private string currentWordOriginal = "";
    private string currentWordFixed = "";
    private bool foundLetter = false;
    int charIndex;

    void Start()
    {
        wordText.text = GetRandomWord();
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void UpdateSelectedWord(string letter, GameObject gameObject)
    {
        string letterFiltered = letter.Contains("Clone") ? letter.Remove(1) : letter;

        // Verifica se colidiu com alguma letra
        if (RandomLetterRespawner.letters.Contains(letterFiltered))
        {

            // Verifica se existe a letra informada na palavra atual
            for (int i = 0; i < currentWordSpplited.Count; i++)
            {
                if (currentWordSpplited[i].Length == 1 && currentWordSpplited[i].ToUpper().Equals(letterFiltered))
                {
                    currentWordSpplited[i] = "<color=yellow>" + currentWordSpplited[i] + "</color>";
                    foundLetter = true;
                    break;
                }
            }

            if(!foundLetter)
            {
                return;
            }

            foundLetter = false;

            currentWordOriginal = "";

            // Atualiza a palavra atual
            foreach (string s in currentWordSpplited)
            {
                currentWordOriginal += s;
            }

            // Atualiza a palavra na tela
            wordText.text = currentWordOriginal;

            // Diminui a quantidade de letras restantes
            wordLettersRemainingToComplete--;

            // teste - remover
            wordLettersRemainingToComplete = 0;

            // Verifica se acabou as letras restantes, ou seja, se a palavra foi completada  
            if (wordLettersRemainingToComplete == 0)
            {
                // Limpa a palavra atual e coloca uma nova palavra na tela
                currentWordOriginal = "";
                wordText.text = GetRandomWord();
                OpenWordImageMenu();
            }

            // Pega os pontos 
            int points = int.Parse(pointsText.text.Remove(0, 8));

            points += 5;

            pointsText.text = "Pontos: " + points;

            // Destrói o objeto de cubo letra 
            if (gameObject)
            {
                Destroy(gameObject);
            }
        }
    }

    public string GetRandomWord()
    {
        // Pega uma palavra aleatoriamente do array selecionado
        int randomIndex = Random.Range(0, animals.Length);

        // Cria uma versão da palavra selecionada para ser verificada
        currentWordSpplited = new List<string>(animals[randomIndex].Split(','));

        // Cria a palavra origial sem as ','
        foreach (string s in currentWordSpplited)
        {
            currentWordOriginal += s;
        }

        // Salva a palavra original para encontrar a imagem correspondente
        currentWordFixed = "cat";
        // currentWordFixed = currentWordOriginal;

        // Atualiza a quantidade de letras restantes
        wordLettersRemainingToComplete = currentWordOriginal.Length;

        return currentWordOriginal;
    }

    public void CloseWordImageMenu()
    {
        wordImageMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void OpenWordImageMenu()
    {
        wordImage.sprite = Resources.Load<Sprite>("Sprites/" + currentWordFixed.ToLower());
        wordImageMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }
}
