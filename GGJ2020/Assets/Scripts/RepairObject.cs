using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairObject : MonoBehaviour
{
    public float damageAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendRepairs(float repairAmount){
        damageAmount -= repairAmount;
    }
}
