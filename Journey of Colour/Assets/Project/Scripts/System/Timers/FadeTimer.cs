using UnityEngine;

public class FadeTimer : CustomTimer
{
    public bool finished;
    private Material[] materials;    
    private float alphaValue, timeToVisible, timeToInvisible;

    //private CustomTimer fadeTimer;
    // Start is called before the first frame update
    public FadeTimer(float fadeTime, float timeToVisible, float timeToInvisible, GameObject gameObject):base(fadeTime)
    {
        alphaValue = -1;
        this.timeToVisible = timeToVisible;
        this.timeToInvisible = timeToInvisible;

        //Search for all the used materials in the object
        materials = new Material[gameObject.GetComponentInChildren<Renderer>().materials.Length];
        materials = gameObject.GetComponentInChildren<Renderer>().materials;
        foreach (Material mat in materials)
        {
            MaterialUtils.SetupBlendMode(mat, MaterialUtils.BlendMode.Transparent);
            mat.SetFloat("_Alpha", alphaValue);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if (start)
        {
            if (alphaValue < 0)
            {
                //to visible
                alphaValue += Time.deltaTime * (1 / timeToVisible);
                ChangeVisibility(alphaValue);
            }
        }

        if (finish)
        {
            if (alphaValue > -1)
            { 
                //to invisible
                alphaValue -= Time.deltaTime * (1 / timeToInvisible);
                ChangeVisibility(alphaValue);
            }
            else 
            {
                finished = true;
            }
        }
        base.Update();
    }

    public override void Reset()
    {
        finished = false;
        base.Reset();
    }

    void ChangeVisibility(float alpha)
    {
        //Sets the alpha visibility of each material
        //Color.a can be used if the object uses a default shader
        foreach (Material mat in materials)
        {
            mat.SetFloat("_Alpha", alpha);
        }
    }
}
