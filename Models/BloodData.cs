namespace ArenaBuilds.Models;

public class BloodData
{
    public bool GiveBloodPotion { get; set; } = false;

    public bool FillBloodPool { get; set; } = false;

    public string PrimaryType { get; set; } = string.Empty;

    public string SecondaryType { get; set; } = string.Empty;

    public float PrimaryQuality { get; set; } = 0;

    public float SecondaryQuality { get; set; } = 0;

    public int SecondaryBuffIndex { get; set; } = 0;
}