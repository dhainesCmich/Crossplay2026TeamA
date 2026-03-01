using UnityEngine;
using System.Collections.Generic;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;

    public List<Recipe> allRecipes = new List<Recipe>();

    private void Awake()
    {
        instance = this;
    }

    public Recipe CheckRecipe(List<Item> inventoryItems)
    {
        foreach (Recipe recipe in allRecipes)
        {
            bool match = true;

            foreach (Food ingredient in recipe.ingredients)
            {
                if (!inventoryItems.Contains(ingredient))
                {
                    match = false;
                    Debug.Log(ingredient + " is not here!");
                    break;
                }
                Debug.Log(ingredient + " is here!");
            }

            if (match)
                return recipe;
        }

        return null;
    }
}