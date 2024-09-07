using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateTaken;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int maxPlatesSpawnedAmount = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if (spawnPlateTimer >= spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if (platesSpawnedAmount < maxPlatesSpawnedAmount)
            {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject() && platesSpawnedAmount > 0)
        {
            platesSpawnedAmount--;
            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
            OnPlateTaken?.Invoke(this, EventArgs.Empty);
        }
    }
}
