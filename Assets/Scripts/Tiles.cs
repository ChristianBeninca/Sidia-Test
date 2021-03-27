using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        child = this.transform.GetChild(0).gameObject;
        child.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        child.SetActive(true);
    }
    private void OnMouseExit()
    {
        child.SetActive(false);

    }
}
