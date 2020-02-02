using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkPlacement : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject cartrigeObject;

    // Start is called before the first frame update
    void Start()
    {
        cartrigeObject.transform.parent = parentObject.transform;
        this.gameObject.transform.parent = parentObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCartrigeObject(GameObject newCart){
        cartrigeObject = newCart;
        cartrigeObject.transform.parent = parentObject.transform;
    }

    public void removeCartrigeObject(){
        cartrigeObject.transform.parent = null;
        cartrigeObject = null;
    }
}
