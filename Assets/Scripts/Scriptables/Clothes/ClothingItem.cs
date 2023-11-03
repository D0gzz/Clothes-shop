using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClothingItem : Item
{
    public bool isEquipped;
    public override void UseItem(){
        EquipClothingItem();
    }

    public abstract void EquipClothingItem();

}
