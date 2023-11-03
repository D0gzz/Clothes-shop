using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
   public bool IsOpen { get; set; }
   [SerializeField] private Animator anim;

   private int amountOfCoinsHolding;

   private void Start()
   {
      amountOfCoinsHolding = Random.Range(1, 10);
   }

   public void OpenChest()
   {
      if (!IsOpen)
      {
         IsOpen = true;
         anim.SetBool("IsOpen", true);
         AudioManager.instance.Play("OpenChest");
         PlayerManager.Instance.FetchInventory().AddCoins(amountOfCoinsHolding);
         gameObject.GetComponentInChildren<Interactable>().IsInteractable = false;
         PlayerManager.Instance.DeNotifyPlayer();
      }
   }
}
