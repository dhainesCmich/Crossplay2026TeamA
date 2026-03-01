using UnityEngine;
using UnityEngine.UI;

public class RecipeMenuUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Button closeButton;
    public Text recipeResultText;
    public GameObject prefabParent;

    private void Awake()
    {
        if(closeButton)
        {
            closeButton.onClick.AddListener(CloseMenu);
        }
    }

    public void TryCook()
    {
        Recipe recipe = RecipeManager.instance.CheckRecipe(Inventory.instance.items);

        if (recipe != null)
        {
            foreach (Food ingredient in recipe.ingredients)
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
        prefabParent.SetActive(false);
    }
}