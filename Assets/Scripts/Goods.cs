using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods : MonoBehaviour
{
    public float price;

    public bool pickedUp = false;

    public enum ProductType
    {
        Cucumber,
        Garlic,
        Carrot,
        BreadRoll,
        Watermelon,
        Apple,
        Croissant,
        Cake,
        Potato,
        Lemon,
        Cherry,
        Bread,
        Onion,
        Pretzel,
        Water,
        EnergyDrink,
        Wine,
        Soda,
        Juice,
        Milk,
        Banana,
        Grape,
        Muffin,
        Zucchini
    }

    public ProductType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp()
    {
        pickedUp = true;
    }
}
