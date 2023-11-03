using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
   [SerializeField] private GameObject shopPanel;

   [SerializeField] private Transform confirmScreen;
   [SerializeField] private List<Item> itemsForSale;
   [SerializeField] private Sprite emptyFieldIcon;

   public bool shopOpened;

   private void Start()
   {
      shopOpened = true;
      CloseConfirm();
      ToggleShop();
   }

   private void InitializeShop()
   {
      int i = 0;
      foreach (Transform gridElement in shopPanel.transform.Find("Grid"))
      {
         Transform buyButton = gridElement.GetChild(0).GetChild(0);
         Transform priceTag = gridElement.GetChild(1);


         if (i < itemsForSale.Count)
         {
            Item item = itemsForSale[i];
            priceTag.gameObject.SetActive(true);
            priceTag.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.priceShop.ToString();
            buyButton.GetComponent<Image>().sprite = item.icon;
            buyButton.GetComponent<Button>().enabled = true;
            buyButton.GetComponent<Button>().onClick.RemoveAllListeners();
            buyButton.GetComponent<Button>().onClick.AddListener(delegate { BuyItem(item); });
            i++;
         }
         else
         {
            priceTag.gameObject.SetActive(false);
            buyButton.GetComponent<Image>().sprite = emptyFieldIcon;
            buyButton.GetComponent<Button>().enabled = false;
         }
      }
   }

   public void BuyItem(Item item)
   {
      OpenConfirm();
      confirmScreen.Find("Question").GetComponent<TextMeshProUGUI>().text = "Do you wish to purchase this item?";
      confirmScreen.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;
      confirmScreen.Find("ItemIcon").GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.priceShop.ToString();
      confirmScreen.Find("NoButton").GetComponent<Button>().onClick.RemoveAllListeners();
      confirmScreen.Find("NoButton").GetComponent<Button>().onClick.AddListener(CloseConfirm);
      confirmScreen.Find("YesButton").GetComponent<Button>().onClick.RemoveAllListeners();
      confirmScreen.Find("YesButton").GetComponent<Button>().onClick.AddListener(delegate { ConfirmPurchase(item); AudioManager.instance.Play("BuyItem");});

   }

   public void ConfirmPurchase(Item item)
   {
      Inventory inventory = PlayerManager.Instance.FetchInventory();
      if (inventory.CoinCount() < item.priceShop)
      {
         Debug.Log("not enough funds");
      }
      else
      {
         inventory.AddItem(item);
         inventory.RemoveCoins(item.priceShop);
      }

      CloseConfirm();
   }

   public void CloseConfirm()
   {
      confirmScreen.gameObject.SetActive(false);
   }

   public void OpenConfirm()
   {
      confirmScreen.gameObject.SetActive(true);
   }

   public void ToggleShop()
   {
      shopOpened = !shopOpened;
      shopPanel.SetActive(shopOpened);
      if (shopOpened)
      {
         InitializeShop();
      }

   }
   public void CloseShop()
   {
      shopOpened = false;
      shopPanel.SetActive(shopOpened);
   }

   public bool ShopOpened()
   {
      return shopOpened;
   }
}
