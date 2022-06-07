using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public GameObject Item;
    public GameObject[] Items;

    public int Cost;
    public Rigidbody2D weapon;
    public Sprite Sprite_right;
    public Sprite Sprite_left;
    public int place;
    public bool heal;

    public void CurrentItemChange()
    {
        if (!Item.activeSelf)
        {
            foreach (GameObject item in Items)
            {
                item.SetActive(false);
            }

            Item.SetActive(true);

            Joueur.ItemCost = Cost;
            Joueur.SelectedWeapon = weapon;
            Joueur.left = Sprite_left;
            Joueur.right = Sprite_right;
            Joueur.endroit = place;
            Joueur.Heal = heal;
        }
    }
}
