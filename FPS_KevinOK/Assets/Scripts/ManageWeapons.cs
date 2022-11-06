using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeapons : MonoBehaviour
{
    Camera playersCamera;
    Ray rayFromPlayer;
    RaycastHit hit;
    public GameObject sparksAtImpact;
    public int gunAmmo = 10;
    public AudioSource[] sounds;
    public AudioSource GunShot;
    public AudioSource AmmoPickUp;
    public ParticleSystem muzzleflash;

    // Start is called before the first frame update
    void Start()
    {
        playersCamera = GetComponent<Camera>();
        sounds = GetComponents<AudioSource>();
        GunShot = sounds[0];
        AmmoPickUp = sounds[1];
    }

    // Update is called once per frame
    void Update()
    {
        rayFromPlayer = playersCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(rayFromPlayer.origin, rayFromPlayer.direction * 100, Color.red);

        if (Input.GetKeyDown(KeyCode.F) && gunAmmo > 0)
        {
            if(Physics.Raycast(rayFromPlayer, out hit, 100))
            {
                print("The object " + hit.collider.gameObject.name + " is in front of the player");
                Vector3 positionOfImpact;
                positionOfImpact = hit.point;
                Instantiate(sparksAtImpact, positionOfImpact, Quaternion.identity);
                GameObject objectTargeted;
                if (hit.collider.gameObject.tag == "target")
                {
                    objectTargeted = hit.collider.gameObject;
                    objectTargeted.GetComponent<ManageNPC>().gotHit();
                }
            }
            gunAmmo--;
            print("You have " + gunAmmo + " bullets left");
            GunShot.Play();
            muzzleflash.Play();

        }



    }         
        public void manageCollisions(ControllerColliderHit hit)
        {
            if (hit.collider.gameObject.tag == "ammo_gun")
            {
                gunAmmo += 5;
                if (gunAmmo > 25) gunAmmo = 25;
                Destroy(hit.collider.gameObject);
                AmmoPickUp.Play();
            }
        }
}
