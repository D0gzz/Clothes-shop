using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
   public int ID;

   public string itemName;
   public string description;
   public int priceShop;

   public int priceSelling;

   public Sprite icon;

   public abstract void UseItem();
}
