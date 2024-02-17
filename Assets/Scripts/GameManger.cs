using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class GameManger : MonoBehaviour
{
    // public GameObject pause_panel;
    // public GameObject[] rays;
    // public Button machine_ammo;
    // public Button machine_buy;
    // public Button shot_ammo;
    // public Button shot_buy;
    // public Button gun_ammo;
    // bool ctr = true;
    // InputDevice controller;
    // float timer;
    public AudioSource gameBackground;
    public AudioSource roundBreak;
    public AudioSource nZombieSound;
    public AudioSource zombieSound;
    public static float n_damage;
    public static float z_damage;
    public static float health;
    public static int points;
    public static int round;
    public static bool gunbl;
    public static bool gunammobl;
    public static bool machinegunbl;
    public static bool machinegunammobl;
    public static bool shotgunbl;
    public static bool shotgunammobl;
    public GameObject panel;
    public Text txt;
    public Text rtxt;
    public int zombieCount;
    private ZombieGenerator zombieGenerator;
    public float gen_timer;
    // Start is called before the first frame update
    void Start()
    {
        gen_timer = 0;
        zombieGenerator = GetComponent<ZombieGenerator>();
        round = 0;
        points = 0;
        health = 1000;
        gunbl = true;
        n_damage = 0;
        z_damage = 0;
    }
    void Update()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("zomb");
        GameObject[] nZombies = GameObject.FindGameObjectsWithTag("n_zomb");
        zombieCount = GameObject.FindGameObjectsWithTag("zomb").Length + GameObject.FindGameObjectsWithTag("n_zomb").Length;
        zombieSound.volume = 0;
        nZombieSound.volume = 0;
        foreach(GameObject zombie in zombies)
        {
            float x = Math.Abs(player.position.x - zombie.transform.position.x);
            float z = Math.Abs(player.position.z - zombie.transform.position.z);
            float v = x + z;
            if(v <= 10)
            {
                zombieSound.volume = 1;
            }
            v = 10 / v;
            if(zombieSound.volume < v)
            {
                zombieSound.volume = v;
            }
        }
        foreach(GameObject zombie in nZombies)
        {
            float x = Math.Abs(player.position.x - zombie.transform.position.x);
            float z = Math.Abs(player.position.z - zombie.transform.position.z);
            float v = x + z;
            if (v <= 10)
            {
                nZombieSound.volume = 1;
            }
            v = 10 / v;
            if (nZombieSound.volume < v)
            {
                nZombieSound.volume = v;
            }
        }
        zombieSound.volume /= 2;
        nZombieSound.volume /= 2;
        if (zombieCount == 0)
        {
            gen_timer += Time.deltaTime;
            gameBackground.volume = 0.005f;
            roundBreak.volume = 0.8f;
        }
        else
        {
            gameBackground.volume = 0.1f;
            roundBreak.volume = 0;
        }
        if (gen_timer > 10)
        {
            round++;
            if(round < 3)
            {
                zombieGenerator.generate_zomb(round * 5, round * 2);
            }
            else
            {
                zombieGenerator.generate_zomb(2, round * 3);
            }
            gen_timer = 0;
        }
        // timer += 0.01f;
        health -= (z_damage + n_damage);
        if (n_damage == 0 && z_damage == 0 && health < 1000)
        {
            health += 0.01f;
        }
       
        if(health <= 0)
        {
            SceneManager.LoadScene("Lose");
        }
        
        /*
        if (ctr)
        {
            List<InputDevice> inputDevices = new List<InputDevice>();
            InputDeviceCharacteristics characteristics = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right;
            InputDevices.GetDevicesWithCharacteristics(characteristics, inputDevices);
            if(inputDevices.Count > 0)
            {
                ctr = false;
                controller = inputDevices[0];
            }
        }
        
        controller.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if ((primaryButtonValue || Input.GetKey(KeyCode.A)) && timer > 1)
        {
            timer = 0;
            Time.timeScale = 1 - Time.timeScale;
            foreach (var ray in  rays)
            {
                ray.SetActive(!pause_panel.activeSelf);
            }
            pause_panel.SetActive(!pause_panel.activeSelf);
        }
        */

        if (points < 10)
        {
            txt.text = "000" + points.ToString();
        }
        else if (points < 100)
        {
            txt.text = "00" + points.ToString();
        }
        else if(points < 1000)
        {
            txt.text = "0" + points.ToString();
        }
        else
        {
            txt.text = points.ToString();
        }

        if (round < 10)
        {
            rtxt.text = "Round: 00" + round.ToString();
        }
        else if (round < 100)
        {
            rtxt.text = "Round: 0" + round.ToString();
        }
        else
        {
            rtxt.text = "Round: " + round.ToString();
        }

        if (n_damage > 0 || z_damage > 0)
        {
            panel.SetActive(true);
        }
        else 
        { 
            panel.SetActive(false);
        }

        if (round > 10)
        {
            SceneManager.LoadScene("Win");
        }

        /*
        if (points >= 100)
        {
            gun_ammo.image.color = new Color(118.0f / 255, 255.0f / 255, 0);
        }
        else
        {
            gun_ammo.image.color = new Color(142.0f / 255, 6.0f / 255, 0);
        }
        if (points >= 400)
        {
            shot_ammo.image.color = new Color(118.0f / 255, 255.0f / 255, 0);
        }
        else
        {
            shot_ammo.image.color = new Color(142.0f / 255, 6.0f / 255, 0);
        }
        if (points >= 800)
        {
            shot_buy.image.color = new Color(118.0f / 255, 255.0f / 255, 0);
        }
        else
        {
            shot_buy.image.color = new Color(142.0f / 255, 6.0f / 255, 0);
        }
        if (points >= 200)
        {
            machine_ammo.image.color = new Color(118.0f / 255, 255.0f / 255, 0);
        }
        else
        {
            machine_ammo.image.color = new Color(142.0f / 255, 6.0f / 255, 0);
        }
        if (points >= 400)
        {
            machine_buy.image.color = new Color(118.0f / 255, 255.0f / 255, 0);
        }
        else
        {
            machine_buy.image.color = new Color(142.0f / 255, 6.0f / 255, 0);
        }
        */

    }
}
