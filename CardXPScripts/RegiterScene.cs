using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegiterScene : MonoBehaviour {

    public InputField nameOnCard;
    public InputField cardNum;
    public InputField expDate;
    public InputField bankName;

    public InputField loginId;
    public InputField pwd;

    public void onRegisterButtonClick()
    {
        nameOnCard.gameObject.SetActive(true);
        cardNum.gameObject.SetActive(true);
        expDate.gameObject.SetActive(true);
        bankName.gameObject.SetActive(true);

        loginId.gameObject.SetActive(false);
        pwd.gameObject.SetActive(false);
    }

    public void onLoginButtonClick()
    {
        loginId.gameObject.SetActive(true);
        pwd.gameObject.SetActive(true);

        nameOnCard.gameObject.SetActive(false);
        cardNum.gameObject.SetActive(false);
        expDate.gameObject.SetActive(false);
        bankName.gameObject.SetActive(false);
    }

    public void onRegisterClick()
    {
        string url = "http://127.0.0.1:8080/ping";

        WWWForm form = new WWWForm();
        form.AddField("cardNumber", cardNum.text);
        form.AddField("ExpiryDate", expDate.text);
        form.AddField("nameOnCard", nameOnCard.text);
        form.AddField("bankName", bankName.text);


        string body = JsonUtility.ToJson(form);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(body);

        Dictionary<string, string> headers = form.headers;
        headers["Content-Type"] = "application/json";

        WWW www = new WWW(url, data, headers);
        // WWW www = new WWW(url, form);


       // StartCoroutine(CreateLoginID(www));

        //authManager.SignUpNewUser(emailInput.text, passwordInput.text);

        Debug.Log("Register");
    }
}
