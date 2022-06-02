using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazerGunItem : MonoBehaviour
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

            ItemBuy.ItemCost = Cost;
            ItemBuy.Item = sprite;
        }
    }
}
