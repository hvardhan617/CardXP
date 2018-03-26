using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InvitesManager : MonoBehaviour {

    //on Invites Button Click
    public void SendInvite()
    {
        Debug.Log("Sending an invitation...");
        var invite = new Firebase.Invites.Invite()
        {
            TitleText = "Experience AR Bank",
            MessageText = "Experience the world of AR in Banking!",
            CallToActionText = "Download it for FREE",
            DeepLinkUrl = new System.Uri("http://google.com/abc")
        };
        Firebase.Invites.FirebaseInvites
         .SendInviteAsync(invite).ContinueWith(HandleSentInvite);
    }

    void HandleSentInvite(Task<Firebase.Invites.SendInviteResult> sendTask)
    {
        if (sendTask.IsCanceled)
        {
            Debug.Log("Invitation canceled.");
        }
        else if (sendTask.IsFaulted)
        {
            Debug.Log("Invitation encountered an error:");
            Debug.Log(sendTask.Exception.ToString());
        }
        else if (sendTask.IsCompleted)
        {

            Debug.Log("SendInvite: " +
            (new List<string>(sendTask.Result.InvitationIds)).Count +
            " invites sent successfully.");
            foreach (string id in sendTask.Result.InvitationIds)
            {
                Debug.Log("SendInvite: Invite code: " + id);
            }
        }
    }
}
