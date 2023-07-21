using System;
using UnityEngine;

namespace Analytics
{
    public class ShopAnalytics
    {
        public string itemName;
        public int butterfliesAfter;
        public ShopAnalytics(string boughtItem, int postButter)
        {
            this.itemName = boughtItem;
            this.butterfliesAfter = postButter;
        }
    }
    
}