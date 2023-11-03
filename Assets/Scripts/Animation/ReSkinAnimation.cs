using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReSkinAnimation : MonoBehaviour
{
   private string outfitName;

   private string hatName;

   private void Start()
   {
      outfitName = "";
      hatName = "";
   }

   private void LateUpdate()
   {
      RenderOutfit();
      RenderHat();
   }

   public void ChangeOutfit(string name)
   {
      outfitName = name;
   }

   public void ChangeHat(string name)
   {
      hatName = name;
   }

   private void RenderOutfit()
   {
      Sprite[] subSpritesOutfits = Resources.LoadAll<Sprite>("Characters/Outfit/" + outfitName);
      if (outfitName == "")
         subSpritesOutfits = null;
      var rendererOutfit = transform.Find("Body").GetComponent<SpriteRenderer>();
      RenderPart(subSpritesOutfits, rendererOutfit);
   }

   private void RenderHat()
   {
      Sprite[] subSpritesHats = Resources.LoadAll<Sprite>("Characters/Hat/" + hatName);
      if (hatName == "")
         subSpritesHats = null;
      var rendererHat = transform.Find("Hat").GetComponent<SpriteRenderer>();
      RenderPart(subSpritesHats, rendererHat);
   }

   private void RenderPart(Sprite[] subsprites, SpriteRenderer renderer)
   {

      if (subsprites != null && subsprites.Length != 0)
      {
         if (renderer.sprite)
         {
            string spriteName = renderer.sprite.name;
            var newSprite = Array.Find(subsprites, item => item.name == spriteName);

            if (newSprite)
            {
               renderer.sprite = newSprite;
            }
         }
      }
      else
      {
         renderer.sprite = null;
      }
   }
}
