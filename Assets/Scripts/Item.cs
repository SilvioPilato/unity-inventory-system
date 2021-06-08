using System;
using UnityEngine;

[CreateAssetMenu(menuName  = "Inventory/Item")]
public class Item: ScriptableObject
{
    [SerializeField]
    private string displayName = "";
    [field: SerializeField]
    [TextArea] 
    private string description = "";
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Sprite sprite;

    public string DisplayName
    {
        get => displayName;
        set => displayName = value;
    }

    public GameObject Prefab
    {
        get => prefab;
        set => prefab = value;
    }
        
    public Sprite Sprite
    {
        get => sprite;
        set => sprite = value;
    }
    
    public string Description
    {
        get => description;
        set => description = value;
    }
}