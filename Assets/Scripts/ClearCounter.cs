using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterSpawnPoint;

    private KitchenObject kitchenObject;
    public void Interact(Player player)
    {
        if (kitchenObject != null)
        {
            kitchenObject.SetKitchenObjectParent(player);
        }
        else
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterSpawnPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
    }

    public Transform GetKitchenObjectTransform()
    {
        return counterSpawnPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
