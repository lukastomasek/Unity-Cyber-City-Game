using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private string Restart = "Restart";

    private void OnGUI()
    {
        if (GUI.Button(new Rect(40, 100, 100, 50), Restart))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

} // end 
