using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCam.transform);    
    }
}
