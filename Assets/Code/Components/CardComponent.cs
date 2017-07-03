using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    private Image background, cost, upper, lower;




    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES
    /* Setters
     * Other
     * */



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    protected override void Start()
    {
        base.Start();

        this.transform.SetParent(GameManager.Canvas.transform, false);
        this.transform.localScale = new Vector3(0.25f, 0.25f);

        background = SpawnNewLayer("Background", "Background", 0);
        cost = SpawnNewLayer("Cost", "Cost1", 2);
        Vector3 backgroundRawExtent = background.sprite.bounds.extents * 100f;

        upper = SpawnNewLayer("Upper", "Attack", 1);   
        upper.transform.localPosition = new Vector3(0, backgroundRawExtent.y/2, 0);
        
        lower = SpawnNewLayer("Lower", "Move", 1);
        lower.transform.localPosition = new Vector3(0, -backgroundRawExtent.y/2, 0);
        lower.transform.localScale = new Vector3(-1, -1, 1);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.Rotate(0, 0, 0.5f);
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    private Image SpawnNewLayer(string partName, string spriteName, int sortingOrder)
    {
        Image r = new GameObject().AddComponent<Image>();
        r.transform.SetParent(this.transform, false);
        r.name = partName;
        r.sprite = GS.LoadCardSprite(spriteName);
        r.SetNativeSize();
        //r.sortingOrder = sortingOrder;
        return r;
    }

}
