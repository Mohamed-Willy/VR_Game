using UltimateXR.Manipulation;
using UnityEngine;

public class BotAssistant : MonoBehaviour
{
    private bool move;
    private Rigidbody rb;
    Animator animator;
    public GameObject gun;
    public GameObject gunammo;
    public GameObject machinegun;
    public GameObject machinegunammo;
    public GameObject shotgun;
    public GameObject shotgunammo;
    public GameObject gunammoCube;
    public GameObject machinegunCube;
    public GameObject machinegunammoCube;
    public GameObject shotgunCube;
    public GameObject shotgunammoCube;
    public UxrGrabbableObject[] grabbableObjects;
    public Transform target;
    public float offsitex;
    public float offsitez;
    public float speed;
    private float timer = 0;
    private float itimer = 0;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {

        if (gunammoCube.GetComponent<UxrGrabbableObject>().IsBeingGrabbed && GameManger.points >= 100)
        {
            GameManger.gunammobl = true;
            GameManger.points -= 100;
        }
        if (machinegunammoCube.GetComponent<UxrGrabbableObject>().IsBeingGrabbed && GameManger.points >= 200)
        {
            GameManger.machinegunammobl = true;
            GameManger.points -= 200;
        }
        if (machinegunCube.GetComponent<UxrGrabbableObject>().IsBeingGrabbed && GameManger.points >= 400)
        {
            GameManger.machinegunbl = true;
            GameManger.points -= 400;
        }
        if (shotgunammoCube.GetComponent<UxrGrabbableObject>().IsBeingGrabbed && GameManger.points >= 400)
        {
            GameManger.shotgunammobl = true;
            GameManger.points -= 400;
        }
        if (shotgunCube.GetComponent<UxrGrabbableObject>().IsBeingGrabbed && GameManger.points >= 800)
        {
            GameManger.shotgunbl = true;
            GameManger.points -= 800;
        }
        if(transform.position.y < -50)
        {
            transform.position = new Vector3(transform.position.x, 60, transform.position.z);
        }

        timer += Time.deltaTime;
        itimer += Time.deltaTime;
        float x = target.position.x - transform.position.x;
        float z = target.position.z - transform.position.z;
        if (x < offsitex && x > -1 * offsitex) x = 0;
        if (z < offsitez && z > -1 * offsitez) z = 0;

        if (x < 0) x = -1;
        else if (x > 0) x = 1;

        if (z < 0) z = -1;
        else if(z > 0) z = 1;
        move = true;
        foreach (UxrGrabbableObject obj in grabbableObjects)
        {
            if (obj.IsBeingGrabbed)
            {
                move = false;
            }
        }

        if((x != 0 || z!= 0) && move)
        {
            animator.SetBool("BoxUp", true);
            animator.SetBool("BoxDown", false);
            if (itimer > 4)
            {
                gun.SetActive(false);
                machinegun.SetActive(false);
                shotgun.SetActive(false);
                gunammo.SetActive(false);
                machinegunammo.SetActive(false);
                shotgunammo.SetActive(false);
                machinegunCube.SetActive(false);
                shotgunCube.SetActive(false);
                gunammoCube.SetActive(false);
                machinegunammoCube.SetActive(false);
                shotgunammoCube.SetActive(false);

                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotationZ;
                rb.constraints = RigidbodyConstraints.FreezeRotationX;
                rb.useGravity = true;
                animator.SetBool("Walking2", true);
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                transform.position = new Vector3(transform.position.x + x * speed * Time.deltaTime / 2, transform.position.y, transform.position.z + z * speed * Time.deltaTime);
            }
            timer = 0;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
            animator.SetBool("Walking2", false);
            animator.SetBool("BoxUp", false);
            animator.SetBool("BoxDown", true);
            if (timer > 3)
            {
                if (GameManger.gunbl)
                {
                    gun.SetActive(true);
                }
                if (GameManger.machinegunbl)
                {
                    machinegun.SetActive(true);
                    machinegunCube.SetActive(false);
                }
                else
                {
                    machinegunCube.SetActive(true);
                    machinegun.SetActive(false);
                }
                if (GameManger.shotgunbl)
                {
                    shotgun.SetActive(true);
                    shotgunCube.SetActive(false);
                }
                else
                {
                    shotgunCube.SetActive(true);
                    shotgun.SetActive(false);
                }
                if (GameManger.gunammobl)
                {
                    gunammo.SetActive(true);
                    gunammoCube.SetActive(false);
                }
                else
                {
                    gunammoCube.SetActive(true);
                    gunammo.SetActive(false);
                }
                if (GameManger.machinegunammobl)
                {
                    machinegunammo.SetActive(true);
                    machinegunammoCube.SetActive(false);
                }
                else
                {
                    machinegunammoCube.SetActive(true);
                    machinegunammo.SetActive(false);
                }
                if (GameManger.shotgunammobl)
                {
                    shotgunammo.SetActive(true);
                    shotgunammoCube.SetActive(false);
                }
                else
                {
                    shotgunammoCube.SetActive(true);
                    shotgunammo.SetActive(false);
                }
            }
            itimer = 0;
        }
    }
}
