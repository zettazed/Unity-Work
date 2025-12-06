using RuStore.PayClient;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RustoreInAppPurchasings : MonoBehaviour
{
    public static RustoreInAppPurchasings Instance;

    [HideInInspector] public UnityEvent PurchaseAction;

    public ProductId[] ProductIds;
    public List<RuStore.PayClient.Product> Products;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GetProducts();
    }

    private void GetProducts()
    {
/*#if !UNITY_EDITOR
        RuStorePayClient.Instance.GetProducts(
        productsId: ProductIds,
        onFailure: (error) => {
            Debug.Log("RuStore Get Product Failed: " + error.name + " " + error.description);
            // Process error
        },
        onSuccess: (result) => {
            Debug.Log("RuStore Get Product Succesfull");
            Products = result;
            // Process success
        });
#endif*/
    }

    public RuStore.PayClient.Product GetProduct(string productId)
    {
#if !UNITY_EDITOR
        foreach (RuStore.PayClient.Product product in Products)
        {
            if (product.productId.value == productId)
                return product;
        }
        return null;
#else
        return null;
#endif
    }

    public void Purchase(UnityEvent purchase)
    {
        Debug.Log("RuStore Purchase Start");
        PurchaseAction = purchase;

        PurchaseAction?.Invoke();
    }
}