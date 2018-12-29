using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotLightRandomizer : MonoBehaviour {

    #region Private Members

    /// <summary>
    /// Color that is supposed to be changed each update
    /// </summary>
    Vector3 slotLightColorHSV;

    /// <summary>
    /// Material which the color will be changed
    /// </summary>
    Material slotLightMaterial;

    #endregion

    #region Public Members
    public float colorChangeSpeed = .5f;
    public float startHue = 0f;
    public float colorAlpha = 0.05f;
    #endregion
    // Use this for initialization
    void Start () {
		var x  = GetComponent<MeshRenderer>();
        slotLightMaterial = x.materials[0];
        float h, s, v;
        Color.RGBToHSV(slotLightMaterial.color, out h, out s, out v);
        slotLightColorHSV = new Vector3(startHue, s, v);
        var tmpColor = Color.HSVToRGB(slotLightColorHSV.x, slotLightColorHSV.y, slotLightColorHSV.z);
        tmpColor.a = colorAlpha;
        slotLightMaterial.color = tmpColor;
    }
	
	// Update is called once per frame
	void Update () {
        if (slotLightMaterial != null)
        {
            slotLightColorHSV.x = (slotLightColorHSV.x  + Time.deltaTime * colorChangeSpeed) % 1f;
            var newColor = Color.HSVToRGB(slotLightColorHSV.x, slotLightColorHSV.y, slotLightColorHSV.z);
            newColor.a = colorAlpha;
            slotLightMaterial.color = newColor;
        }
        else
        {
            Debug.LogWarning("There's no slot light material attached to this object");
        }
    }
}
