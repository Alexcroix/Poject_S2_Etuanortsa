using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuy : MonoBehaviour
{
    public static int ItemCost;
    public static Sprite Item;

    public void Buy()
    {
        Joueur.BuyWeapon(ItemCost, Item);
    }
}
