using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DayNightManager : MonoBehaviour
{
    [SerializeField] private Light dirLight;
    [SerializeField] private DayNightLightPreset preset;
    [SerializeField, Range(0, 24)] private float timeOfDay;
    [SerializeField] private float intervallSetting = 0.1f;

    public bool autoRefresh = false;

    private void Update()
    {
        if (preset == null)
            return;
        
        if(Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * intervallSetting;
            timeOfDay %= 24;
            LightingSettings(timeOfDay/24f);
        }
        else
        {
        }
    }

    private void LightingSettings(float perc)
    {
        UnityEngine.RenderSettings.ambientLight = preset.AmbientColor.Evaluate(perc);
        UnityEngine.RenderSettings.fogColor = preset.Fogcolor.Evaluate(perc);

        if (dirLight != null)
        {
            dirLight.color = preset.DirectionalColor.Evaluate(perc);
            dirLight.transform.localRotation = Quaternion.Euler(new Vector3((perc * 360f) - 90f, 170f, 0));
        }
        
    }

    public void SetNewSetting()
    {
        LightingSettings(timeOfDay / 24);
    }



    private void OnValidate()
    {
        if (dirLight != null)
            return;
        if(UnityEngine.RenderSettings.sun != null)
        {
            dirLight = UnityEngine.RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();

            foreach (Light light in lights)
            {
                if(light.type == UnityEngine.LightType.Directional)
                {
                    dirLight = light;
                    return;
                }
            }

        }      

    }

}
