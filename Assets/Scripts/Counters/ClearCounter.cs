using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            } else {
                //Player is Carrying Something
                if (player.GetKitchenObject() is PlateKitchenObject) {
                    //Player is holding a plate
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                } else {
                    //Player is not carrying plate
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
    }    
}
