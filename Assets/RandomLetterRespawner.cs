using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomLetterRespawner : MonoBehaviour
{
    public int xPos;
    public int yPos;
    public int zPos;
    public int lettersQuantity = 100;
    public int indexOfLetters = 0;
    public static string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private char letterSelected;
    public static string lettersCubDefaultPath = "Prefabs/";
    public int[] xCoords = new int[2];
    public int[] yCoords = new int[2];
    public int[] zCoords = new int[2];
    public GameObject[] cubeLetters;
    public GameObject[] cubeLettersInstance;
    public bool canInstantiateNextLetters = false;
    public GameManager gameManager;

    void Start()
    {
        // Instancia o array dos cubos com o tamanho referente a quantidade de letras informada
        cubeLetters = new GameObject[lettersQuantity];
        cubeLettersInstance = new GameObject[lettersQuantity];

        xCoords = new int[2] { 6, 998 };
        yCoords = new int[2] { 1, 2 };
        zCoords = new int[2] { 6, 998 };

    }

    private void Update()
    {
        if(canInstantiateNextLetters)
        {
            indexOfLetters = 0;
            // Executa o instanciamento das letras
            StartCoroutine(SpawnRandom());
            canInstantiateNextLetters = false;
        }
    }

    IEnumerator SpawnRandom()
    {
        
        for(int i = 0; i < lettersQuantity; i++)
        {
            // Limites de terreno para instanciar os objetos
            xPos = Random.Range(xCoords[0], xCoords[1]);
            yPos = Random.Range(yCoords[0], yCoords[1]);
            zPos = Random.Range(zCoords[0], zCoords[1]);

            // Verifica se acabou a lista de letras da palavra selecionada, e se sim, começa do início da palavra
            if(indexOfLetters >= gameManager.currentWordFixed.Length)
            {
                indexOfLetters = 0;
            }

            // Pega a letra selecionada
            letterSelected = gameManager.currentWordFixed.ToUpper()[indexOfLetters];

            // Carrega o objeto da letra selecionada
            cubeLetters[i] = Resources.Load(lettersCubDefaultPath + "Blue/TOYBlock_Blue_" + letterSelected + "_LP") as GameObject;

            // Incrementa o contador de letras
            indexOfLetters++;

            // Altera o nome do objeto de texto para verificar a colisão
            cubeLetters[i].name = letterSelected.ToString();

            // Instancia a letra criada nas posições aleatórias geradas
            cubeLettersInstance[i] = Instantiate(cubeLetters[i], new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DestroyAllWordObjects()
    {
        canInstantiateNextLetters = false;

        for(int i = 0; i < cubeLettersInstance.Length; i++)
        {
            if(cubeLettersInstance[i])
            {
                Destroy(cubeLettersInstance[i]);
            }
        }
        canInstantiateNextLetters = true;
    }
}
