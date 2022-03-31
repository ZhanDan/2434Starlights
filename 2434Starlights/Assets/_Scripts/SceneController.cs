using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string prevScene;
    public string nextScene;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(prevScene != "")
            {
                SceneManager.LoadScene(prevScene);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if(nextScene != "")
            {
                SceneManager.LoadScene(nextScene);

            }
        }
    }
}
