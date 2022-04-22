using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items
{
    public enum ItemType
    {
        GUN,
        LAZER,
        MACHINE_GUN
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
                case ItemType.GUN: return 0;
                case ItemType.LAZER: return 1;
                case ItemType.MACHINE_GUN: return 2;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.GUN: return GameAssets.i.sprite;
            case ItemType.LAZER: return GameAssets.i.sprite;
            case ItemType.MACHINE_GUN: return GameAssets.i.sprite;
        }
    }
}
