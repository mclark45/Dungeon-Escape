using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _shop;
    private Player _player;
    private int _itemSelected;
    private int _costOfItem;
    private int _selectedItemImgPos = -550;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (_player == null)
            Debug.LogError("Player script is Null");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UIManager.Instance.UpdatePlayerGemCount(_player.Gems());
            UIManager.Instance.UpdateShopSelection(_selectedItemImgPos);
            _player.ShopOpen(true);

            _shop.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _shop.SetActive(false);
            _player.ShopOpen(false);
        }
    }

    public void SelectItem(int item)
    {
        _itemSelected = item;

        switch (item)
        {
            case 0: //flame sword
                _selectedItemImgPos = 83;
                UIManager.Instance.UpdateShopSelection(_selectedItemImgPos);
                _costOfItem = 200;
                break;
            case 1: //boots of flight
                _selectedItemImgPos = -15;
                UIManager.Instance.UpdateShopSelection(_selectedItemImgPos);
                _costOfItem = 400;
                break;
            case 2: //key to castle
                _selectedItemImgPos = -121;
                UIManager.Instance.UpdateShopSelection(_selectedItemImgPos);
                _costOfItem = 100;
                break;
        }
    }

    public void BuyItem()
    {
        switch (_itemSelected)
        {
            case 0: //flame sword
                _player.UsedGems(_costOfItem);
                break;
            case 1: //boots of flight
                _player.UsedGems(_costOfItem);
                break;
            case 2: //key to castle
                _player.UsedGems(_costOfItem);
                break;
        }
    }
}
