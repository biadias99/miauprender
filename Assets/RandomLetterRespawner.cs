using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomLetterRespawner : MonoBehaviour
{
    public GameObject letter;
    public int xPos;
    public int yPos;
    public int zPos;
    public int lettersQuantity = 30;
    public int indexOfLetters = 0;
    public static string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private char letterSelected;

    int randomInt;
    void Start()
    {
       // Executa o instanciamento das letras
       StartCoroutine(SpawnRandom());
    }

    IEnumerator SpawnRandom()
    {
        for(int i = 0; i < lettersQuantity; i++)
        {
            // Limites de terreno para instanciar os objetos
            xPos = Random.Range(-58, 70);
            yPos = Random.Range(4, 5);
            zPos = Random.Range(-75, 67);

            // Verifica se acabou a lista de palavras, e se sim, começa do início do alfabeto
            if(indexOfLetters >= letters.Length)
            {
                indexOfLetters = 0;
            }

            // Pega a letra selecionada
            letterSelected = letters[indexOfLetters];

            // Incrementa o contador de letras
            indexOfLetters++;

            // Altera a letra a ser exibida
            letter.GetComponent<TextMeshPro>().text = letterSelected.ToString();

            // Altera o nome do objeto de texto para verificar a colisão
            letter.name = letterSelected.ToString();

            // Instancia a letra criada nas posições aleatórias geradas
            Instantiate(letter, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
