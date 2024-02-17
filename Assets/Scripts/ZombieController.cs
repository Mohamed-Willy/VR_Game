using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float offsitex;
    public float offsitez;
    public float speed;
    private Transform target;
    private Animator animator;
    private int health;
    private bool damage;
    private void Start()
    {
        damage = false;
        if(gameObject.tag == "n_zomb")
        {
            health = 4;
        }
        else
        {
            health = 2;
        }
        animator = GetComponent<Animator>();
    }
    private void Update()
    { 
        if(health <= 0)
        {
            if(gameObject.tag == "n_zomb")
            {
                GameManger.points += 50;
            }
            else
            {
                GameManger.points += 25;
            }
            if (damage)
            {
                if (gameObject.tag == "n_zomb")
                {
                    GameManger.n_damage -= 3;
                }
                else
                {
                    GameManger.z_damage -= 1;
                }
                damage = false;
            }
            Destroy(gameObject);
            return;
        }
        if(transform.position.y < -200)
        {
            Destroy(gameObject);
        }

        target = GameObject.FindGameObjectWithTag("Player").transform;
        float x = target.position.x - transform.position.x;
        float z = target.position.z - transform.position.z;

        if (x < offsitex && x > -1 * offsitex) x = 0;
        if (z < offsitez && z > -1 * offsitez) z = 0;

        if (x < 0) x = -1;
        else if (x > 0) x = 1;

        if (z < 0) z = -1;
        else if (z > 0) z = 1;

        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        if (x != 0 || z != 0)
        {
            if (damage)
            {
                if (gameObject.tag == "n_zomb")
                {
                    GameManger.n_damage -= 3;
                }
                else 
                {
                    GameManger.z_damage -= 1;
                }
                damage = false;
            }
            animator.SetBool("attack", false);
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        else
        {
            animator.SetBool("attack", true);
            if (damage == false)
            {
                if (gameObject.tag == "n_zomb")
                {
                    GameManger.n_damage += 3;
                }
                else
                {
                    GameManger.z_damage += 1;
                }
                damage = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "shotgunbullet")
        {
            health -= 10;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "machinegunbullet")
        {
            health -= 2;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "gunbullet")
        {
            health -= 3;
            Destroy(other.gameObject);
        }
    }
}
