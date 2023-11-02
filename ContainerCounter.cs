using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform CounterTopPoint;
    private KitchenObject kitchenObject;

    public override void Interact(Player player) {
            Transform kitchenObjectTransfrom = Instantiate(kitchenObjectSO.prefab, CounterTopPoint);
            kitchenObjectTransfrom.GetComponent<KitchenObject>().SetKitchenObjectParent(player);


    }

    public Transform GetkitchenObjectFollowTransform() {
        return CounterTopPoint;
    }


    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
    public void ClearKitchenObject() {
        kitchenObject = null;
    }
    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
