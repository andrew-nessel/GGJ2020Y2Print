using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkAmount : MonoBehaviour
{
    public float cyanEmpty = 0;
    public float magentaEmpty = 0;
    public float yellowEmpty = 0;
    public float blackEmpty = 0;
    [SerializeField] ColorRepair color = null;
    // Start is called before the first frame update
    void Start()
    {
        if(color!=null){
            color.LoadCartridge(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateInk(float cyanChange, float magentaChange, float yellowChange, float blackChange){
        cyanEmpty = cyanChange;
        magentaEmpty = magentaChange;
        yellowEmpty = yellowChange;
        blackEmpty = blackChange; 
    }

    public void UpdateColorRepair(ColorRepair cr){
        color = cr;
    }
    public void RemoveSelf(){
        if(color != null){
            color.RemoveCartridge();
        }
        color = null;
    }
}
