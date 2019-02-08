using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{

    public GameObject bullet;
    
    void Start()
    {
        
    }

    void Update()
    {
        
        
    }

    public override void Fire()
    {
        Instantiate(bullet,bulletSpawnPosition.position,Quaternion.identity);
    }
}
