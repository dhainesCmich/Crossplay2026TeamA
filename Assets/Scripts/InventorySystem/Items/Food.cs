using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : Item
{
    [Header("Food Properties")]
    [Min(0)]
    public int durability = 1;

    // Optional: Called when used
    public override void Use()
    {
        Debug.Log("Using food: " + name);

        durability--;

        if (durability <= 0)
        {
            RemoveFromInventory();
        }
    }
}