using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    [Range(50, 1000)]   
    public float sens;  
    public Transform body; 
    float xrot = 0f;

    public Transform leaner;
    public float zrot;
    bool isrotating;

    public float smothing;
    float currentrot;


    private void Start()
    {
       Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
            float rotx=Input.GetAxisRaw("Mouse X")*sens*Time.deltaTime; 
        float roty = Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime; 

        xrot -= roty;  
       xrot = Mathf.Clamp(xrot, -80f, 80f);

        currentrot += rotx;
        currentrot=Mathf.Lerp(currentrot,0,smothing*Time.deltaTime);


        transform.localRotation = Quaternion.Euler(xrot, 0f, currentrot);
         
        body.Rotate(Vector3.up * rotx);

        #region Q/E  SÝDE LOOK
        if (Input.GetKey(KeyCode.E))
        {
            zrot = Mathf.Lerp(zrot, -20.0f, 5f *Time.deltaTime);
            isrotating = true;

        }
        if (Input.GetKey(KeyCode.Q))
        {
         
            zrot = Mathf.Lerp(zrot, 20.0f, 5f * Time.deltaTime);
            isrotating = true;
    
        }
        if (Input.GetKeyUp(KeyCode.Q)|| Input.GetKeyUp(KeyCode.E)) 
        {
            isrotating = false;
        }
        if(!isrotating)

            zrot = Mathf.Lerp(zrot, 0f,5f * Time.deltaTime);
        
        leaner.localRotation=Quaternion.Euler(0,0,zrot);
    }
    #endregion


}
