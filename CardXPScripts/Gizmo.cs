using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gizmo : MonoBehaviour {

    public float gizmoSize = 0.75f;
    public Color gizmoColor = Color.yellow;

    public GameObject paymentPopup;
    public GameObject confirmationPopup;
    public Text paymentStatus;


    public GameObject redeemNowPopup;
    public Text redeemStatusText;


    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }

    public void OnPaynowButtonClick()
    {
        paymentPopup.SetActive(false);
        confirmationPopup.SetActive(true);
    }

    public void OnCancelButtonClick()
    {
        paymentPopup.SetActive(false);
    }

    public void onConfirmPaymentClick()
    {
        paymentStatus.text = "Thank You!!Payment Received";
        StartCoroutine(ClosePopup());
    }

    public IEnumerator ClosePopup()
    {
        yield return new WaitForSeconds(2);
        paymentStatus.text = "";
        confirmationPopup.SetActive(false);
        
    }

    public void onCancelPaymentClick()
    {
        confirmationPopup.SetActive(false);
        
    }

    public void onBillPayClick()
    {
        paymentPopup.SetActive(true);

    }

    public void onRedeemButtonClick()
    {
        redeemNowPopup.SetActive(true);
    }
    public void onRedeemNowConfirmation()
    {

        redeemStatusText.text = "Your Redeem Request has been raised!!";

    }

    public void onRedeemNowCancellation()
    {
        
        redeemNowPopup.SetActive(false);

    }
}
