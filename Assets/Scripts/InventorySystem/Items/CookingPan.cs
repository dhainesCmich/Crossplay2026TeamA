using UnityEngine;

public class CookingPan : Interactable
{
    public GameObject recipeMenuUI;

    public override void Interact()
    {
        base.Interact();
        OpenRecipeMenu();
    }

    void OpenRecipeMenu()
    {
        recipeMenuUI.SetActive(true);
    }
}