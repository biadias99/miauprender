using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Text wordText;
    public Text pointsText;
    public TMP_Text finishGamePointsText;
    public TMP_Text completedWordText;
    public GameObject wordImageMenuUI;
    public GameObject finishGameMenuUI;
    public Image wordImage;
    public RandomLetterRespawner randomLetterRespawner;
    public PauseMenu pauseMenu;
    public int wordLettersRemainingToComplete;
    static public string[] animals = new string[] { "C,a,t", "D,o,g", "B,e,e", "A,n,t", "M,o,u,s,e", "H,a,m,s,t,e,r", "R,a,b,b,i,t", "F,i,s,h", "C,r,a,b", "S,h,a,r,k", "D,o,l,p,h,i,n", "F,o,x", "W,o,l,f", "R,h,i,n,o", "K,o,a,l,a", "C,h,e,e,t,a,h", "Z,e,b,r,a", "L,i,o,n", "M,o,n,k,e,y", "G,i,r,a,f,f,e" };
    static public string[] numbers = new string[] { "O,n,e", "T,w,o", "T,h,r,e,e", "F,o,u,r", "F,i,v,e", "S,i,x", "S,e,v,e,n", "E,i,g,h,t", "N,i,n,e", "T,e,n" };
    static public string[] food = new string[] { "P,a,s,t,a", "C,h,i,c,k,e,n", "P,i,e", "C,a,k,e", "P,i,z,z,a", "M,i,l,k", "J,u,i,c,e", "E,g,g,s", "M,e,a,t", "C,h,e,e,s,e", "B,r,e,a,d" };
    static public string[] fruits = new string[] { "A,p,p,l,e", "B,a,n,a,n,a", "P,e,a,r", "P,i,n,e,a,p,p,l,e", "G,r,a,p,e,s", "K,i,w,i", "L,i,m,e", "C,h,e,r,r,y", "P,e,a,c,h", "M,e,l,o,n", "T,o,m,a,t,o", "W,a,t,e,r,m,e,l,o,n" };
    static public string[] colors = new string[] { "P,u,r,p,l,e", "P,i,n,k", "B,l,a,c,k", "G,r,e,e,n", "R,e,d", "Y,e,l,l,o,w", "W,h,i,t,e", "O,r,a,n,g,e", "B,r,o,w,n", "G,r,e,y", "B,l,u,e" };
    static public string[] currentCategoryWords;
    List<string> currentWordSpplited;
    public string currentWordOriginal = "";
    public string currentWordFixed = "";
    private bool foundLetter = false;
    public int wordsCompleted = 0;
    public int wordsToFinishGame = 2;
    public int points = 0;

    void Start()
    {
        pauseMenu.canPauseMenu = true;
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
           //  wordLettersRemainingToComplete = 0;

            // Verifica se acabou as letras restantes, ou seja, se a palavra foi completada  
            if (wordLettersRemainingToComplete == 0)
            {

                // Atualiza a quantidade de palavras completadas
                wordsCompleted++;

                // Mostra a imagem da palavra completada
                OpenWordImageMenu();

                if (wordsCompleted < wordsToFinishGame)
                {
                    // Limpa a palavra atual e coloca uma nova palavra na tela
                    currentWordOriginal = "";
                    wordText.text = GetRandomWord();
                }
               
            }

            // Pega os pontos 
            points = int.Parse(pointsText.text.Remove(0, 8));

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
        int randomIndex = Random.Range(0, currentCategoryWords.Length);

        // Cria uma versão da palavra selecionada para ser verificada
        currentWordSpplited = new List<string>(currentCategoryWords[randomIndex].Split(','));

        // Cria a palavra origial sem as ','
        foreach (string s in currentWordSpplited)
        {
            currentWordOriginal += s;
        }

        // Salva a palavra original para encontrar a imagem correspondente
        currentWordFixed = currentWordOriginal;

        // currentWordFixed = currentWordOriginal;

        // Atualiza a quantidade de letras restantes
        wordLettersRemainingToComplete = currentWordOriginal.Length;

        randomLetterRespawner.DestroyAllWordObjects();

        return currentWordOriginal;
    }

    public void CloseWordImageMenu()
    {
        pauseMenu.canPauseMenu = true;
        if (wordsCompleted == wordsToFinishGame)
        {
            // Jogo concluído!
            wordImageMenuUI.SetActive(false);
            OpenFinishGameMenu();
        }
        else
        {
            wordImageMenuUI.SetActive(false);
            Time.timeScale = 1;
            PauseMenu.GameIsPaused = false;
            PauseMenu.canVerifyPauseMenu = true;
        }
    }

    public void OpenWordImageMenu()
    {
        pauseMenu.canPauseMenu = false;
        wordImage.sprite = Resources.Load<Sprite>("Sprites/" + currentWordFixed.ToLower());
        wordImage.preserveAspect = true;
        completedWordText.text = currentWordFixed;
        wordImageMenuUI.SetActive(true);
        Time.timeScale = 0;
        PauseMenu.GameIsPaused = true;
        PauseMenu.canVerifyPauseMenu = true;
    }

    public void OpenFinishGameMenu()
    {
        pauseMenu.canPauseMenu = false;
        finishGamePointsText.text = "Pontuação final: " + points;
        finishGameMenuUI.SetActive(true);
        Time.timeScale = 0;
        PauseMenu.GameIsPaused = true;
        PauseMenu.canVerifyPauseMenu = true;
    }

    public void PlayGameAgain()
    {
        Time.timeScale = 1;
        PauseMenu.GameIsPaused = false;
        PauseMenu.canVerifyPauseMenu = true;
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMenu()
    {
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu");
    }
}
