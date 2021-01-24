using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryMenu : MonoBehaviour
{
    public void SelectCategory(string category)
    {
        switch (category)
        {
            case "animals":
                GameManager.currentCategoryWords = GameManager.animals;
                break;
            case "numbers":
                GameManager.currentCategoryWords = GameManager.numbers;
                break;
            case "food":
                GameManager.currentCategoryWords = GameManager.food;
                break;
            case "fruits":
                GameManager.currentCategoryWords = GameManager.fruits;
                break;
            case "colors":
                GameManager.currentCategoryWords = GameManager.colors;
                break;
            default:
                GameManager.currentCategoryWords = GameManager.animals;
                break;
        }
    }
}
