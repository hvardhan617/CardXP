using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntegrationEndPoints : MonoBehaviour {

    public InputField uid;
    public InputField paymentAmount;
    public InputField currentBalance;


    public void onPayNowClickRestCall()
    {
        string url = "http://192.168.43.26:8080/payNow";

        WWWForm form = new WWWForm();
        form.AddField("currentBalance", currentBalance.text);
        form.AddField("paymentAmount", paymentAmount.text);

        form.AddField("uid", uid.text);

        // form.AddField("bankName", nameOfBank.text);


        string body = JsonUtility.ToJson(form);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(body);

        Dictionary<string, string> headers = form.headers;
        headers["Content-Type"] = "application/json";

        WWW www = new WWW(url, data, headers);
        // WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
    }
        IEnumerator WaitForRequest(WWW www)
        {
            yield return www;

        if (www.error != null)
        {
            Debug.Log("Response Received from Bank API::::" + www.text);
            
        }
        else
        {
            Debug.Log("WWW Error::::Error in Service hit to Bank API " + www.error);
        }
        // WWW www = new WWW(url, form);


        //StartCoroutine(CreateLoginID(www));

        //authManager.SignUpNewUser(emailInput.text, passwordInput.text);

        Debug.Log("Register");
    }
}
