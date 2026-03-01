using UnityEngine;
using UnityEngine.UI;

public class RecipeMenuUI : MonoBehaviour
{
    public Text recipeResultText;

    public void TryCook()
    {
        Recipe recipe = RecipeManager.instance.CheckRecipe(Inventory.instance.items);

        if (recipe != null)
        {
            foreach (FoodItem ingredient in recipe.ingredients)
            {
                Inventory.instance.Remove(ingredient);
            }

            Inventory.instance.Add(recipe.result);

            recipeResultText.text = "Cooked: " + recipe.result.name;
        }
        else
        {
            recipeResultText.text = "No valid recipe!";
        }
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}