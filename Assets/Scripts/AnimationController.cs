using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationController : MonoBehaviour
{
    public GameObject player;
    Animator anim;
    public float actionDist, speed;
    // public AnimationClip[] animClips = new AnimationClip[2];
    //private bool isDoorOpen = false;
    public Transform[] targetArray;
    private float playerLoc, location;




    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        location = transform.position.z;
        if (player != null)
        {
            playerLoc = player.transform.position.z;
        }
       

        if (location - playerLoc <= actionDist /*&&  !isDoorOpen*/)
        {
           // Debug.Log(gameObject.name + "is activated");
            int indexNumber = Random.Range(0, 1);
           
            Transform target = targetArray[indexNumber];

            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            //isDoorOpen = true;
        }
    }
}
