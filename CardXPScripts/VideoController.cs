using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour {

    //   public GameObject videoPlayer;

    //public void PlayVideo()
    //   {
    //       videoPlayer.SetActive(true);
    //   }

    public GameObject videoPlayer;

    //public Canvas canvas;


    // Use this for initialization
    void Start()
    {
        //video.SetActive(false);
      //  canvas.gameObject.SetActive(false);
       videoPlayer.SetActive(false);
    }

// Update is called once per frame
void Update()
    {

    }

    public void onButtonClick()
    {
        Debug.Log("on Loan Products Click");
        videoPlayer.SetActive(true);
    }

    public void InitializeUI()
    {
      //  canvas.gameObject.SetActive(true);
    }

}
