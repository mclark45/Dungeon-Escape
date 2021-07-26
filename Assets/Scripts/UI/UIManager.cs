using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is Null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] private Text _gemCountText;
    [SerializeField] private Text _gemCountHUD;
    [SerializeField] private Image _selectionImage;
    [SerializeField] private Image[] _healthBar;

    public void UpdatePlayerGemCount(int gems)
    {
        _gemCountText.text = gems + "G";
    }

    public void Score(int gems)
    {
        _gemCountHUD.text = "" + gems;
    }

    public void UpdateShopSelection(int yPos)
    {
        _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void HealthHUD(int health)
    {
        switch (health)
        {
            case 0:
                _healthBar[0].enabled = false;
                break;
            case 1:
                _healthBar[1].enabled = false;
                break;
            case 2:
                _healthBar[2].enabled = false;
                break;
            case 3:
                _healthBar[3].enabled = false;
                break;
            default:
                return;
        }
    }
}
