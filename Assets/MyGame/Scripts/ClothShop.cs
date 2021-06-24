using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ClothShop : MonoBehaviour
{
    #region || ----- Fields & Properties ----- ||

    public static ClothShop Instance;

    private Animator animator;

    /// <summary>
    /// All Cloths that are Available in Shop to Sell.
    /// </summary>
    [SerializeField] List<Cloth> availableCloths;

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject helpPanel;

    [SerializeField] private GameObject itemPrefeb;

    [Header("----- Buy Cloth -----"), Space(5)]
    [SerializeField] private GameObject buyClothPanel;
    [SerializeField] private Transform buyClothItemHolder;
    [SerializeField] private GameObject selectedItemPanel;
    [SerializeField] private Text selectedItemName;
    [SerializeField] private Text selectedItemPrice;
    private Cloth clothToBuy;

    [Header("----- Sell Cloth -----"), Space(5)]
    [SerializeField] private GameObject sellClothPanel;
    [SerializeField] private Transform sellClothItemHolder;
    [SerializeField] private GameObject selectedItemPanelOfSell;
    [SerializeField] private Text selectedItemNameOfSell;
    [SerializeField] private Text selectedItemPriceOfSell;
    private Cloth clothToSell;

    #endregion

    // -------------------------------------------------------------------------

    #region || ----- MonoBehaviour Methods ----- ||

    private void Awake ()
    {
        Assert.IsNotNull(panel);
        Assert.IsNotNull(helpPanel);
        Assert.IsNotNull(itemPrefeb);

        Assert.IsNotNull(buyClothPanel);
        Assert.IsNotNull(buyClothItemHolder);
        Assert.IsNotNull(selectedItemPanel);
        Assert.IsNotNull(selectedItemName);
        Assert.IsNotNull(selectedItemPrice);

        Assert.IsNotNull(sellClothPanel);
        Assert.IsNotNull(sellClothItemHolder);
        Assert.IsNotNull(selectedItemPanelOfSell);
        Assert.IsNotNull(selectedItemNameOfSell);
        Assert.IsNotNull(selectedItemPriceOfSell);

        if ( Instance != this ) Instance = this;

        animator = GetComponent<Animator>();
    }

    private void Start ()
    {
        if ( panel.activeSelf ) panel.SetActive(false);
        if ( helpPanel.activeSelf ) helpPanel.SetActive(false);
        if ( buyClothPanel.activeSelf ) buyClothPanel.SetActive(false);
    }

    #endregion

    #region || ----- ClothShop Methods ----- ||

    /// <summary>
    /// Called when Player Stand Upon Enterance.
    /// </summary>
    public void OnPlayerEnter ()
    {
        if ( helpPanel.activeSelf ) helpPanel.SetActive(false);
        if ( buyClothPanel.activeSelf ) buyClothPanel.SetActive(false);
        if ( sellClothPanel.activeSelf ) sellClothPanel.SetActive(false);
        if ( !panel.activeSelf ) panel.SetActive(true);

        animator.SetTrigger("OnPlayerEnter");
    }

    /// <summary>
    /// Called After Player Enters Shop.
    /// </summary>
    public void AfterPlayerEntered ()
    {
        GameManager.Instance.DisablePlayerMovement();
        helpPanel.SetActive(true);
    }

    public void OnClick_Exit ()
    {
        if ( helpPanel.activeSelf ) helpPanel.SetActive(false);
        if ( buyClothPanel.activeSelf ) buyClothPanel.SetActive(false);
        if ( sellClothPanel.activeSelf ) sellClothPanel.SetActive(false);

        animator.SetTrigger("OnPlayerExit");
    }

    public void AfterPlayerExit ()
    {
        GameManager.Instance.EnablePlayerMovement();
        panel.SetActive(false);
    }

    public void OnClick_BuyClothsButton ()
    {
        if ( sellClothPanel.activeSelf ) sellClothPanel.SetActive(false);
        helpPanel.SetActive(false);
        buyClothPanel.SetActive(true);
        LoadBuyClothData();
    }

    private void LoadBuyClothData ()
    {
        for ( int i = 0; i < buyClothItemHolder.childCount; i++ )
        {
            Destroy(buyClothItemHolder.GetChild(i).gameObject);
        }

        foreach ( var item in availableCloths )
        {
            var button = Instantiate(itemPrefeb);
            button.transform.GetChild(0).GetComponent<Text>().text = item.name;
            button.GetComponent<Button>().onClick.AddListener(() => OnClick_SelectClothItem(item));
            button.transform.SetParent(buyClothItemHolder);
        }

        if ( selectedItemPanel.activeSelf ) selectedItemPanel.SetActive(false);
    }

    private void OnClick_SelectClothItem ( Cloth cloth )
    {
        selectedItemName.text = cloth.name;
        selectedItemPrice.text = "Buy $" + cloth.price;
        clothToBuy = cloth;
        if ( !selectedItemPanel.activeSelf ) selectedItemPanel.SetActive(true);
    }

    public void OnClick_BuySelectedItem ()
    {
        availableCloths.Remove(clothToBuy);
        GameManager.Instance.BuyCloth(clothToBuy);
        LoadBuyClothData();
    }

    public void OnClick_SellClothsButton ()
    {
        if ( buyClothPanel.activeSelf ) buyClothPanel.SetActive(false);
        helpPanel.SetActive(false);
        sellClothPanel.SetActive(true);
        LoadSellClothData();
    }

    private void LoadSellClothData ()
    {
        for ( int i = 0; i < sellClothItemHolder.childCount; i++ )
        {
            Destroy(sellClothItemHolder.GetChild(i).gameObject);
        }

        foreach ( var item in GameManager.Instance.ClothsToSell )
        {
            var button = Instantiate(itemPrefeb);
            button.transform.GetChild(0).GetComponent<Text>().text = item.name;
            button.GetComponent<Button>().onClick.AddListener(() => OnClick_SelecteClothItemToSell(item));
            button.transform.SetParent(sellClothItemHolder);
        }

        if ( selectedItemPanelOfSell.activeSelf ) selectedItemPanelOfSell.SetActive(false);
    }

    private void OnClick_SelecteClothItemToSell ( Cloth cloth )
    {
        selectedItemNameOfSell.text = cloth.name;
        selectedItemPriceOfSell.text = "Sell $" + cloth.price;
        clothToSell = cloth;
        if ( !selectedItemPanelOfSell.activeSelf ) selectedItemPanelOfSell.SetActive(true);
    }

    public void OnClick_SellSelectedItem ()
    {
        availableCloths.Add(clothToSell);
        GameManager.Instance.SellCloth(clothToSell);
        LoadSellClothData();
        GameManager.Instance.WearClothToPlayer(GameManager.Instance.MyCloths[0]);
    }

    #endregion
}