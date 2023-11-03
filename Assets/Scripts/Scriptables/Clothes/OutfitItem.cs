using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Outfit", menuName = "Items/Clothing/Outfit")]
public class OutfitItem : ClothingItem
{
   public override void EquipClothingItem()
   {
      PlayerManager.Instance.ChangeOutfit(this);
   }
}
