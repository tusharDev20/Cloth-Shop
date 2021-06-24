using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ChangeCloths : MonoBehaviour
{
    #region || ----- Fields & Properties ----- ||

    [SerializeField] private GameObject panel;
    [SerializeField] private Transform itemsHolder;
    [SerializeField] private GameObject itemPrefeb;

    #endregion

    // ------------------------------------------------------------------------------

    #region || ----- MonoBehaviour Methods ----- ||

    private void Awake ()
    {
        Assert.IsNotNull(panel);
        Assert.IsNotNull(itemsHolder);
        Assert.IsNotNull(itemPrefeb);

        if ( panel.activeSelf ) panel.SetActive(false);
    }

    #endregion

    #region || ----- ChangeCloths Methods ----- ||

    public void OpenChangeClothsScreen ()
    {
        panel.SetActive(true);
        LoadPlyerCloth();
    }

    public void CloseChangeClothsScreen ()
    {
        panel.SetActive(false);
    }

    private void LoadPlyerCloth ()
    {
        for ( int i = 0; i < itemsHolder.childCount; i++ )
        {
            Destroy(itemsHolder.GetChild(i).gameObject);
        }

        foreach ( var item in GameManager.Instance.MyCloths )
        {
            var button = Instantiate(itemPrefeb);
            button.transform.GetChild(0).GetComponent<Text>().text = item.name;
            button.GetComponent<Button>().onClick.AddListener(() => WearClothToPlayer(item));
            button.transform.SetParent(itemsHolder);
        }
    }

    public void WearClothToPlayer ( Cloth cloth )
    {
        GameManager.Instance.WearClothToPlayer(cloth);
        CloseChangeClothsScreen();
    }

    #endregion
}