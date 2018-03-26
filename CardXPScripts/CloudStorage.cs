using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudStorage : MonoBehaviour
{

    string url = "https://firebasestorage.googleapis.com/v0/b/cardxp-217cc.appspot.com/o/bms-ib-offer-d.jpg?alt=media&token=b9f31531-fecb-48a7-992f-02f2ac5011a2";
    Texture2D img;

    public RawImage image;
    // Use this for initialization
     public void fetchImage()
    {
        StartCoroutine(LoadImg());
    }

    IEnumerator LoadImg()
    {
        yield return 0;
        WWW imgLink = new WWW(url);
        yield return imgLink;
        img = imgLink.texture;
        image.texture = img;
    }
    
   
}
