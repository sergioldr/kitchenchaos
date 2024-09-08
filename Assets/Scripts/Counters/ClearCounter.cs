using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                bool hasAddedIngredient = plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO());

                if (hasAddedIngredient)
                {
                    GetKitchenObject().DestroySelf();
                }
            }
            else if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
            {
                bool hasAddedIngredientToPlate = plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO());

                if (hasAddedIngredientToPlate)
                {
                    player.GetKitchenObject().DestroySelf();
                }
            }
        }
    }
}
