using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkAmount : MonoBehaviour
{
    public float cyanEmpty = 0;
    public float magentaEmpty = 0;
    public float yellowEmpty = 0;
    public float blackEmpty = 0;
    [SerializeField] ColorRepair color = null;

    public Slider cyanSlider;
    public Slider magentaSlider;
    public Slider yellowSlider;
    public Slider blackSlider;

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
        cyanSlider.value = (100 - cyanEmpty)/100;
        magentaSlider.value = (100 - magentaEmpty)/100;
        yellowSlider.value = (100 - yellowEmpty)/100;
        blackSlider.value = (100 - blackEmpty)/100;
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
