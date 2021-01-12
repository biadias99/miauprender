using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text wordText;
    public Text pointsText;
    public int wordLettersRemainingToComplete;
    public string[] animals = new string[] { "Cat", "Dog", "Bee", "Ant", "Mouse", "Hamster", "Rabbit", "Fish", "Crab", "Shark", "Dolphin", "Fox", "Wolf", "Rhino", "Koala", "Cheetah", "Zebra", "Lion", "Monkey", "Giraffe" };
    public string[] numbers = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
    public string[] food = new string[] { "Pasta", "Chips", "Chicken", "Pie", "Cake", "Pizza", "Noodles", "Milk", "Juice", "Eggs", "Meat", "Cheese", "Bread" };
    public string[] fruits = new string[] { "Apple", "Banana", "Pear", "Pineapple", "Grapes", "Kiwi", "Lime", "Cherry", "Peach", "Melon", "Tomato" };
    public string[] colors = new string[] { "Purple", "Pink", "Black", "Green", "Red", "Yellow", "White", "Orange", "Brown", "Grey", "Blue" };
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
            // TODO: pensar melhor nisso!!!
            for (int i = 0; i < wordText.text.Length; i++)
            {
                if (RandomLetterRespawner.letters.Contains(wordText.text[i].ToString()))
                {
                    Debug.Log("I ------------- " + wordText.text[i] + " " + i);
                    if(i-1 >= 0 && i-2 >= 0)
                    {
                        Debug.Log("I - 1 ------------- " + (i-1) + "  " + wordText.text[i-1]);
                        Debug.Log("verify -> " + (wordText.text[i - 1].Equals('>') && wordText.text[i - 2].Equals('r')));
                        if (wordText.text[i-1].Equals('>') && wordText.text[i-2].Equals('r'))
                        {
                            charIndex = wordText.text.ToUpper().IndexOf(letterFiltered.ToUpper());
                        }
                    }
                    else if(i == 0)
                    {
                        charIndex = wordText.text.ToUpper().IndexOf(letterFiltered.ToUpper());
                    }
                }
            }

            Debug.Log("CHAR INDEX -> " + charIndex);

            if (charIndex != -1)
            {
                // Altera a cor da letra informada

                string newLetter = "<color=yellow>" + wordText.text[charIndex] + "</color>";
                string newWord = "";

                for(int i = 0; i < wordText.text.Length; i++)
                {
                    if(i == charIndex)
                    {
                        newWord = string.Concat(newWord, newLetter);
                    }
                    else
                    {
                        newWord = string.Concat(newWord, wordText.text[i].ToString());

                    }
                }

                wordText.text = newWord;

                wordLettersRemainingToComplete--;

                if(wordLettersRemainingToComplete == 0)
                {
                    wordText.text = GetRandomWord();
                }

                // Pega os pontos 
                int points = int.Parse(pointsText.text.Remove(0, 8));
                Debug.Log("points -> " + points);

                points += 5;

                Debug.Log("points after -> " + points);
                pointsText.text = "Pontos: " + points;

                if (gameObject)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public string GetRandomWord()
    {
        int randomIndex = Random.Range(0, animals.Length);
        wordLettersRemainingToComplete = animals[randomIndex].Length;
        //  return animals[randomIndex];
        return "AAA";
    }
}
