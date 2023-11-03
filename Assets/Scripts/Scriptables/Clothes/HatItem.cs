using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hat", menuName = "Items/Clothing/Hat")]
public class HatItem : ClothingItem
{
   public override void EquipClothingItem()
   {
      PlayerManager.Instance.ChangeHat(this);
   }
}
