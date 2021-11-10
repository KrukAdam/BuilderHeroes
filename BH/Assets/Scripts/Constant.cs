
public static class Constant
{
    //Sorting layer
    //Map start position > y = 0 but position map  < y = 9999 
    public static int GroundOrderLayer = -9999;
    public static int GroundOverlayOrderLayer = -9990;
    public static int LowCollidersOrderLayer = -9995;
    public static int HighCollidersOrderLayer = -9994;

    public static int ItemOnMapOrderLayer = 10010;
    public static int PlayerStartOrderLayer = 10020;
    public static int MissileStartOrderLayer = 10021;

    //LAYER MASK ID
    public static int LowLayerColliders = 10;  

    //Stats
    public static int MinDamage = 1;

    //Skills
    //Push logic
    public static float TimeToBlockMove = 1f;
    public static float PushPowerMultiplier = 100f;
}
