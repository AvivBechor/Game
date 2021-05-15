using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EVILopacityController : MonoBehaviour
{
    public Image opacityPanelImage;
    public LayerMask evilGround;

    void Start()
    {
        opacityPanelImage = GameObject.Find("EVILpanel").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, .2f, evilGround))
        {
            Debug.Log("ELI");
            if (opacityPanelImage.color.a < 0.4)
                opacityPanelImage.color = new Color(opacityPanelImage.color.r, opacityPanelImage.color.g, opacityPanelImage.color.b, opacityPanelImage.color.a + 0.5f * Time.deltaTime);
        }
        else
        {
            if (opacityPanelImage.color.a > 0)
                opacityPanelImage.color = new Color(opacityPanelImage.color.r, opacityPanelImage.color.g, opacityPanelImage.color.b, opacityPanelImage.color.a - 0.5f * Time.deltaTime);
        }
    }
}
