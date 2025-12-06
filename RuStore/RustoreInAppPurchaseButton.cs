using RuStore.PayClient;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RustoreInAppPurchaseButton : MonoBehaviour
{
    [SerializeField] private string _productId;
    [SerializeField] TMP_Text priceText;
    public UnityEvent _purchase;

    private void Start()
    {
        /*RuStore.PayClient.Product product = RustoreInAppPurchasings.Instance.GetProduct(_productId);
        if (product != null)
            priceText.text = product.price.value + " " + product.amountLabel.value;
        else
            priceText.text = "Ru";*/
    }

    public void Purchase()
    {
//#if !UNITY_EDITOR
        var parameters = new ProductPurchaseParams(
        productId: new ProductId(_productId),
        appUserEmail: null,
        appUserId: null,
        developerPayload: null,
        orderId: null,
        quantity: new Quantity(1)
        );

        var sdkTheme = SdkTheme.LIGHT;

        RuStorePayClient.Instance.Purchase(
            parameters: parameters,
            preferredPurchaseType: PreferredPurchaseType.ONE_STEP,
            sdkTheme,
            onFailure: (error) => {
                switch (error)
                {
                    case RuStorePaymentException.ProductPurchaseCancelled cancelled:
                        Debug.Log("RuStore Purchase Cancelled");
                        break;
                    case RuStorePaymentException.ProductPurchaseException exception:
                        Debug.Log("RuStore Purchase Failed");
                        break;
                    default:
                        Debug.Log("RuStore Purchase Other Error: " + error.description);
                        break;
                }
            },
            onSuccess: (result) => {
                Debug.Log("RuStore Purchase Other Error");
                RustoreInAppPurchasings.Instance.Purchase(_purchase);
            });
//#endif
    }
}