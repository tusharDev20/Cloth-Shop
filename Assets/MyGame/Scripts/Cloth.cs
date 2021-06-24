using UnityEngine;

[CreateAssetMenu(fileName = "Cloth", menuName = "Cloth-Shop/Cloth")]
public class Cloth : ScriptableObject
{
    #region || ----- Fields & Properties ----- ||

    public string name;
    public Material material;
    public int price;

    #endregion
}