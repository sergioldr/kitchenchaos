using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnProgressChangedEventArgs> OnIngredientAdded;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {

        if (!validKitchenObjectSOList.Contains(kitchenObjectSO) || kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }

        kitchenObjectSOList.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnProgressChangedEventArgs { kitchenObjectSO = kitchenObjectSO });
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
