using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
[CreateAssetMenu(fileName="DayNight Preset", menuName="Scriptables/DayNight Preset", order=1)]

public class DayNightLightPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient Fogcolor;
}
