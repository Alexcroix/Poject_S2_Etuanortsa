using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public GameObject Item;
    public GameObject[] Items;

    public int Cost;
    public Sprite sprite;

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
            //Joueur.Item = sprite;
        }
    }
}
