using UnityEngine;

public class ClothShopEnterance : MonoBehaviour
{
    #region || ----- Fields & Properties ----- ||

    private bool canEnterInShop;

    #endregion

    // ------------------------------------------------------------------------

    #region || ----- MonoBehaviour Methods ----- ||

    private void Awake ()
    {
        canEnterInShop = true;
    }

    private void OnTriggerEnter ( Collider collider )
    {
        if ( collider.tag == "Player" )
        {
            if ( canEnterInShop )
            {
                canEnterInShop = false;
                ClothShop.Instance.OnPlayerEnter();
            }
        }
    }

    private void OnTriggerExit ( Collider collider )
    {
        if ( collider.tag == "Player" )
        {
            if ( !canEnterInShop ) canEnterInShop = true;
        }
    }

    #endregion

    #region || ----- ClothShopEnterance Methods ----- ||

    #endregion
}