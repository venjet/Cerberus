using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    dogCoin,
}

public class Item : MonoBehaviour
{

    public ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemGot(Player player){
        Destroy(gameObject);
        switch(itemType){
            case ItemType.dogCoin:
                player.getDogCoin();
                break;
        }
    }
}