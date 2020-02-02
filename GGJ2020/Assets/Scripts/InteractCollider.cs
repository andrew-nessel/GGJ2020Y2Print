using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollider : MonoBehaviour
{

    public float playerRepairAmount;
    public float playerRepairCooldown;
    public float playerPickupCooldown;
    public float currentRepairCooldown = 0f;
    public float currentPickupCooldown = 0f;
    public GameObject pickupLocation;
    public bool isHolding;
    public GameObject heldObject;
    public GameObject snapLocation;

    public AudioSource Pickup;
    public AudioSource Repair;
    public AudioClip pickupSFX;
    public AudioClip[] repairSFX;

    void Start(){
      Pickup.clip = pickupSFX;
    }

    void Update(){
        if(isHolding){
            if(Input.GetButton("Pickup") && (currentPickupCooldown <= 0f)){
                Material mat = heldObject.GetComponent<Renderer>().material;
                SetOpaque(mat);
                if(snapLocation == null){
                    heldObject.transform.position = pickupLocation.transform.position + pickupLocation.transform.forward*15 + new Vector3(0f, 3.5f, 0f);
                    Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                    heldObject.transform.parent = null;
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    Debug.Log(rb.velocity);
                    heldObject = null;
                    isHolding = false;
                    currentPickupCooldown = playerPickupCooldown;
                }else{
                    heldObject.transform.position = snapLocation.transform.position;
                    InkAmount cart = heldObject.GetComponent<InkAmount>();
                    snapLocation.GetComponent<InkPlacement>().addCartrigeObject(heldObject);
                    snapLocation.GetComponent<MeshRenderer>().enabled = false;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject = null;
                    isHolding = false;
                    currentPickupCooldown = playerPickupCooldown;
                    ColorRepair ink = snapLocation.GetComponent<ColorRepair>();
                    snapLocation = null;
                    if(ink != null && cart != null){
                        ink.LoadCartridge(cart);
                    }
                }
            }
        }
        if(currentRepairCooldown > 0f){
            currentRepairCooldown -= Time.deltaTime;
        }

        if(currentPickupCooldown > 0f){
            currentPickupCooldown -= Time.deltaTime;
        }

    }


    private void OnTriggerStay(Collider other){
        GameObject otherGO = other.gameObject;
        if(string.Equals(otherGO.tag, "Repairable")){
            if(Input.GetButton("Repair") && (!isHolding)){
                if(currentPickupCooldown <= 0f){
                    Repair.clip = repairSFX[Random.Range(0,repairSFX.Length)];
                    Repair.Play();
                    RepairObject repairO = otherGO.GetComponent<RepairObject>();
                    repairO.sendRepairs(playerRepairAmount);
                    currentPickupCooldown = playerRepairCooldown;
                }
            }
        }
        if(string.Equals(otherGO.tag, "Pickupable")){
            if(Input.GetButton("Pickup") && (!isHolding) && (currentPickupCooldown <= 0f)){
                otherGO.transform.position = pickupLocation.transform.position;
                heldObject = otherGO;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                isHolding = true;
                Pickup.Play();
                currentPickupCooldown = playerPickupCooldown;
                InkAmount cart = heldObject.GetComponent<InkAmount>();
                if(snapLocation != null){
                    snapLocation.GetComponent<InkPlacement>().removeCartrigeObject();
                }
                otherGO.transform.parent = pickupLocation.transform;
                if(cart != null){
                    cart.RemoveSelf();
                }
                Material mat = heldObject.GetComponent<Renderer>().material;
                SetFade(mat);
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, .5f);
            }
        }
    }

    private void OnTriggerEnter(Collider other){
        GameObject otherGO = other.gameObject;
        if(string.Equals(otherGO.tag, "PlaceLocation")){
            if(isHolding){
                otherGO.GetComponent<MeshRenderer>().enabled = true;
                snapLocation = otherGO;
            }
        }
    }


    private void OnTriggerExit(Collider other){
        GameObject otherGO = other.gameObject;
        if(string.Equals(otherGO.tag, "PlaceLocation")){
            if(isHolding){
                otherGO.GetComponent<MeshRenderer>().enabled = false;
                snapLocation = null;
            }
        }
    }

    //changing rendering mode things, parts of code taken from https://answers.unity.com/questions/1004666/change-material-rendering-mode-in-runtime.html

    void SetOpaque(Material mat){
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        mat.SetInt("_ZWrite", 1);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = -1;
    }
    void SetFade(Material mat){
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }
}
