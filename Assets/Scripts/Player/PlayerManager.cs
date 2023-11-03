using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   [SerializeField] private GameObject notificationObject;
   [SerializeField] private ShopManager shopManager;
   [SerializeField] private Inventory inventory;

   public static PlayerManager Instance { get; private set; }

   private OutfitItem currentOutfit;
   private HatItem currentHat;
   private void Awake()
   {
      if (Instance != null && Instance != this)
      {
         Destroy(this);
      }
      else
      {
         Instance = this;
      }
   }

   public void NotifyPlayer()
   {
      notificationObject.SetActive(true);
   }

   public void DeNotifyPlayer()
   {
      notificationObject.SetActive(false);
   }

   public Inventory FetchInventory()
   {
      return inventory;
   }

   public void ChangeOutfit(OutfitItem outfit)
   {
      if (outfit != null)
      {
         gameObject.GetComponent<ReSkinAnimation>().ChangeOutfit(outfit.itemName);
         currentOutfit = outfit;
      }else
      {
         gameObject.GetComponent<ReSkinAnimation>().ChangeOutfit("");
         currentOutfit = null;
      }


   }

   public void ChangeHat(HatItem hat)
   {
      if (hat != null)
      {
         gameObject.GetComponent<ReSkinAnimation>().ChangeHat(hat.itemName);
         currentHat = hat;
      }else
      {
         gameObject.GetComponent<ReSkinAnimation>().ChangeHat("");
         currentHat = null;
      }
   }

   public bool IsPlayerInShop()
   {
      return shopManager.ShopOpened();
   }

   public void OpenCloseShop()
   {
      inventory.ToggleInventory(true);
      shopManager.ToggleShop();
   }

   public void CloseShop()
   {
      shopManager.CloseShop();
      inventory.CloseInventory();
   }

   public OutfitItem getCurrentOutfit()
   {
      return currentOutfit;
   }

   public HatItem getCurrrentHat()
   {
      return currentHat;
   }
}
