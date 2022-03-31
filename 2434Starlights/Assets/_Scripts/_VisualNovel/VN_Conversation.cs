using UnityEngine;
using UnityEngine.UI;

public class VN_Conversation : VN_Interactable
{
    [Header("Conversation Pass Values")]
    public GameObject dialogueUI;
    public Text dialogueText;
    public VN_Speaker[] dialogueThread;

    public VN_Speaker prevSpeaker = null;
    private int threadCounter = 0;

    public override void Interact()
    {
        Debug.Log("interacted");
        if (isActive)
        {
            if(threadCounter == 0)
            {
                StartConversation();
            }

            if(threadCounter < dialogueThread.Length)
            {
                if (prevSpeaker.Speaker)
                {
                    Debug.Log("intawefawesgawsergawerg");
                    prevSpeaker.Speaker.SetActive(false);
                }
                if (dialogueThread[threadCounter].Speaker)
                {
                    dialogueThread[threadCounter].Speaker.SetActive(true);
                }
                dialogueText.text = dialogueThread[threadCounter].dialogue;
                prevSpeaker = dialogueThread[threadCounter];
                threadCounter += 1;
            }
            else
            {
                EndConversation();
            }
        }
    }

    void StartConversation()
    {
        StartInteraction();
        Debug.Log("convo started");
        dialogueUI.SetActive(true);
        GameObject[] convoAvatars = GameObject.FindGameObjectsWithTag("ConvoAvatar");
        foreach(GameObject convAva in convoAvatars)
        {
            convAva.SetActive(false);
        }
    }

    void EndConversation()
    {
        EndInteraction();
        Debug.Log("convo ended");
        dialogueUI.SetActive(false);
        //gameObject.SetActive(false);
    }

    [System.Serializable]
    public class VN_Speaker
    {
        public GameObject Speaker;
        public string dialogue;
    }
}
