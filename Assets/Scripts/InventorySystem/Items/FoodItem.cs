using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food Item")]
public class FoodItem : Item
{
    [Header("Food Properties")]
    public int nutritionValue;
    public bool isCooked;

    public override void Use()
    {
        Debug.Log("Eating " + name + " | Nutrition: " + nutritionValue);
        RemoveFromInventory();
    }
}