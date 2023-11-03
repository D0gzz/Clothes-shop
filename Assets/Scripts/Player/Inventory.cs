using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI coinCount;
   [SerializeField] private GameObject inventoryUI;
   [SerializeField] private Transform confirmScreen;
   [SerializeField] private Sprite emptyFieldIcon;
   public List<Item> items = new List<Item>();
   private int coins;
   private const int MAXITEMS = 10;

   private const KeyCode key = KeyCode.Tab;

   private bool invOpen;

   private void Start()
   {
      invOpen = true;
      ToggleInventory(false);
   }

   private void Update()
   {
      if (Input.GetKeyDown(key) && !PlayerManager.Instance.IsPlayerInShop())
      {
         ToggleInventory(false);
      }
   }

   public void ToggleInventory(bool inShop)
   {
      if (invOpen && inShop)
      {
         if (PlayerManager.Instance.IsPlayerInShop())
         {
            invOpen = false;
         }
         else
         {
            invOpen = true;
         }
      }
      else
      {
         invOpen = !invOpen;
      }

      inventoryUI.SetActive(invOpen);
      RefreshUI(inShop);
   }

   public void CloseInventory()
   {
      invOpen = false;
      inventoryUI.SetActive(invOpen);
   }
   public void AddItem(Item item)
   {
      if (items.Count == MAXITEMS)
      {

      }

      items.Add(item);
      RefreshUI(true);
   }

   public void RemoveItem(Item item)
   {
      if (items.Contains(item))
      {
         items.Remove(item);
      }
      RefreshUI(true);
   }
   public void RefreshUI(bool inShop)
   {
      int i = 0;
      foreach (Transform invElement in inventoryUI.transform.Find("Grid"))
      {
         Transform invButton = invElement.GetChild(0).GetChild(0);
         if (i < items.Count)
         {
            Item item = items[i];
            invButton.GetComponent<Image>().sprite = item.icon;
            invButton.GetComponent<Button>().enabled = true;
            Debug.Log(inShop);
            if (inShop)
            {
               invButton.GetComponent<Button>().onClick.RemoveAllListeners();
               invButton.GetComponent<Button>().onClick.AddListener(delegate { SellItem(item); });
            }
            else
            {
               invButton.GetComponent<Button>().onClick.RemoveAllListeners();
               invButton.GetComponent<Button>().onClick.AddListener(delegate { item.UseItem(); AudioManager.instance.Play("ItemEquip"); });
            }

            i++;
         }
         else
         {
            invButton.GetComponent<Image>().sprite = emptyFieldIcon;
            invButton.GetComponent<Button>().enabled = false;
         }
      }
   }

   public void SellItem(Item item)
   {
      OpenConfirm();
      confirmScreen.Find("Question").GetComponent<TextMeshProUGUI>().text = "Do you wish to sell this item?";
      confirmScreen.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;
      confirmScreen.Find("ItemIcon").GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.priceSelling.ToString();
      confirmScreen.Find("NoButton").GetComponent<Button>().onClick.RemoveAllListeners();
      confirmScreen.Find("NoButton").GetComponent<Button>().onClick.AddListener(CloseConfirm);
      confirmScreen.Find("YesButton").GetComponent<Button>().onClick.RemoveAllListeners();
      confirmScreen.Find("YesButton").GetComponent<Button>().onClick.AddListener(delegate { ConfirmSell(item); AudioManager.instance.Play("SellItem"); });
   }

   private void ConfirmSell(Item item)
   {
      AddCoins(item.priceSelling);
      RemoveItem(item);
      if (PlayerManager.Instance.getCurrentOutfit() == item)
      {
         PlayerManager.Instance.ChangeOutfit(null);
      }
      else if (PlayerManager.Instance.getCurrrentHat() == item)
      {
         PlayerManager.Instance.ChangeHat(null);
      }
      RefreshUI(true);
      CloseConfirm();
   }

   private void CloseConfirm()
   {
      confirmScreen.gameObject.SetActive(false);
   }

   private void OpenConfirm()
   {
      confirmScreen.gameObject.SetActive(true);
   }

   public void RefreshCoinCount()
   {
      coinCount.text = coins.ToString();
   }
   public void AddCoins(int amount)
   {
      coins += amount;
      RefreshCoinCount();
   }

   public void RemoveCoins(int amount)
   {
      coins = coins <= amount ? 0 : coins - amount;
      RefreshCoinCount();
   }

   public int CoinCount()
   {
      return coins;
   }

}
