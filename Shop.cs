using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public Item[] items;
    public Item Buy(string buyable) {
        foreach(var item in items) {
            if(item.name == buyable) {
                return item;
            }
        }
        return new Item();
    }
}
