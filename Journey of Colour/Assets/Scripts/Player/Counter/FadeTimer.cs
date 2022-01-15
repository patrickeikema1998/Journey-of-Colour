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
                alphaValue += Time.deltaTime * (1 / timeToVisible);
                ChangeVisibility(alphaValue);
            }
        }

        if (finish)
        {
            if (alphaValue > -1)
            {
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
        foreach (Material mat in materials)
        {
            mat.SetFloat("_Alpha", alpha);
        }
    }
}
