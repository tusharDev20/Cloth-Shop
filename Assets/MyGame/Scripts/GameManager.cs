using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    #region || ----- Fields & Properties ----- ||

    public static GameManager Instance;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Renderer playerCloth;

    [SerializeField] private List<Cloth> myCloths;

    public List<Cloth> MyCloths
    {
        get { return myCloths; }
    }

    public List<Cloth> ClothsToSell
    {
        get
        {
            var cloths = new List<Cloth>(myCloths);
            cloths.RemoveAt(0);
            return cloths;
        }
    }


    #endregion

    // -------------------------------------------------------------------------

    #region || ----- MonoBehaviour Methods ----- ||

    private void Awake ()
    {
        Assert.IsNotNull(playerMovement);
        Assert.IsNotNull(playerCloth);

        if ( Instance != this )
        {
            Instance = this;
        }
    }

    #endregion

    #region || ----- GameManager Methods ----- ||

    public void DisablePlayerMovement ()
    {
        playerMovement.enabled = false;
    }

    public void EnablePlayerMovement ()
    {
        playerMovement.enabled = true;
    }

    public void BuyCloth ( Cloth cloth )
    {
        myCloths.Add(cloth);
    }

    public void SellCloth ( Cloth cloth )
    {
        myCloths.Remove(cloth);
    }

    public void WearClothToPlayer ( Cloth cloth )
    {
        playerCloth.material = cloth.material;
    }

    #endregion
}