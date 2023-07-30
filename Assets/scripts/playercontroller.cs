using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    AudioSource source;
    CharacterController Controller; 
    Vector3 velocity; 
    bool isgrounded; 
    bool ismoving;


    public Transform ground; 
    public float distance = 0.3f;  
    public float speed; 
    public float jumpheight; 
    public float gravity; 
    public float orginalheight;
    public float crouchheight;


    float timer;
    public float timebewtweensteps;


    public AudioClip[] stepsound;


    public LayerMask mask;
    private void Start()
    {
        Controller = GetComponent<CharacterController>(); 
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        #region movement
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        Controller.Move(move*speed*Time.deltaTime);

        #endregion

        #region footsteps
        if(horizontal!=0||vertical!=0) 
            ismoving = true;
        else ismoving = false;
        if (ismoving&&isgrounded)
        {
            timer -= Time.deltaTime;
          

            if (timer <= 0)
            {
                timer = timebewtweensteps;
                source.clip = stepsound[Random.Range(0, stepsound.Length)];
                source.pitch = Random.Range(0.85f, 1.15f);
                source.Play();
             
            }
        }
        else
        {
            timer = timebewtweensteps;

        }



        #endregion

        #region jump
        if (Input.GetKeyDown(KeyCode.Space)&& isgrounded)  
        {
            velocity.y += Mathf.Sqrt(jumpheight * -3.0f * gravity);
        }


        #endregion
        #region gravity
        isgrounded = Physics.CheckSphere(ground.position, distance, mask);
        if(isgrounded&&velocity.y<0 ) {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
       Controller.Move(velocity*Time.deltaTime);

        #endregion

        #region crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Controller.height = crouchheight;
            speed = 2.0f;
            jumpheight = 0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Controller.height = orginalheight;
            speed = 5.0f;
            jumpheight = 2f;
             
        }


        #endregion

        #region run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 8f;
            timebewtweensteps = 0.3f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
            timebewtweensteps = 0.5f;
             
        }


        #endregion



    }
}
