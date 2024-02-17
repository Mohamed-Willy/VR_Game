using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject fast_zombie;
    public GameObject slow_zombie;
    public GameObject player;
    
    public void generate_zomb(int slow, int fast)
    {
        for (int i = 0; i < slow; i++)
        {
            float x, z;
            x = Random.Range(-1, 2);
            if (x < 0)
            {
                x = Random.Range(-300, -200);
            }
            else if (x > 0)
            {
                x = Random.Range(200, 300);
            }
            z = Random.Range(-1, 2);
            if (z < 0)
            {
                z = Random.Range(-300, -200);
            }
            else if (z > 0 || x == 0)
            {
                z = Random.Range(200, 300);
            }
            GameObject zomb = Instantiate(slow_zombie, new Vector3(player.transform.position.x + x / 2, 60, player.transform.position.z + z / 2), Quaternion.identity);
            zomb.SetActive(true);
        }
        for (int i = 0; i < fast; i++)
        {

            float x, z;
            x = Random.Range(-1, 2);
            if (x < 0)
            {
                x = Random.Range(-300, -200);
            }
            else if (x > 0)
            {
                x = Random.Range(200, 300);
            }
            z = Random.Range(-1, 2);
            if (z < 0)
            {
                z = Random.Range(-300, -200);
            }
            else if (z > 0 || x == 0)
            {
                z = Random.Range(200, 300);
            }
            GameObject zomb = Instantiate(fast_zombie, new Vector3(player.transform.position.x + x, 50, player.transform.position.z + z), Quaternion.identity);
            zomb.SetActive(true);
        }
    }
}
