using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
   public bool IsInRange { get; set; }

   public bool IsInteractable { get; set; }

   private const KeyCode interactKey = KeyCode.E;

   public UnityEvent interactAction;

   public UnityEvent exitAction;

   private void Start()
   {
      IsInteractable = true;
   }
   private void Update()
   {
      if (IsInRange && IsInteractable)
      {
         if (Input.GetKeyDown(interactKey))
         {
            interactAction.Invoke();
         }
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player") && IsInteractable)
      {
         IsInRange = true;
         other.gameObject.GetComponent<PlayerManager>().NotifyPlayer();
      }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player") && IsInteractable)
      {
         IsInRange = false;
         other.gameObject.GetComponent<PlayerManager>().DeNotifyPlayer();
         if(exitAction.GetPersistentEventCount() != 0){
            exitAction.Invoke();
         }
      }
   }
}
