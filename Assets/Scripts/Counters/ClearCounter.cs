using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Không có KitchenObject nào ở đây
            if (player.HasKitchenObject())
            {
                // Người chơi đang cầm một thứ gì đó
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Người chơi không cầm gì cả
            }
        }
        else
        {
            // Có một KitchenObject ở đây
            if (player.HasKitchenObject())
            {
                // Người chơi đang cầm một thứ gì đó
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Người chơi đang cầm một cái đĩa
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        KitchenObject.DestroyKitchenObject(GetKitchenObject());
                    }
                }
                else
                {
                    // Người chơi không cầm đĩa mà là thứ khác
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // Counter đang giữ một cái đĩa
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            KitchenObject.DestroyKitchenObject(player.GetKitchenObject());
                        }
                    }
                }
            }
            else
            {
                // Người chơi không cầm gì cả
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
