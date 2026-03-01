using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Cooking/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public FoodItem[] ingredients;
    public FoodItem result;
}