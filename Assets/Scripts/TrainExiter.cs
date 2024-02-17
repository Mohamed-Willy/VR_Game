using System;
using UltimateXR.Manipulation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainExiter : MonoBehaviour
{
    private UxrGrabbableObject grabbableObject;
    public bool exit = false;
    public bool train = false;
    private void Start()
    {
        grabbableObject = GetComponent<UxrGrabbableObject>();
    }
    void Update()
    {
        if(grabbableObject.IsBeingGrabbed && !train && !exit)
        {
            SceneManager.LoadScene("story");
        }
        else if (grabbableObject.IsBeingGrabbed && train && !exit)
        {
            SceneManager.LoadScene("Training");
        }
        else if (grabbableObject.IsBeingGrabbed && !train && exit)
        {
            Application.Quit();
        }
    }
}
