
using UnityEngine;

public static class Constant
{

    //Sorting layer
    //Max 32767;
    //Map start position > y = 0 but position map  < y = 9999 
    public static int GroundOrderLayer = -9999;
    public static int GroundOverlayOrderLayer = -9990;
    public static int LowCollidersOrderLayer = -9995;
    public static int HighCollidersOrderLayer = -9994;

    public static int ItemOnMapOrderLayer = 10010;
    public static int BaseStartOrderLayer = 10020;
    public static int MissileStartOrderLayer = 10021;
    public static int BlueprintBuildingOrderLayer = 32760;


    //LAYER MASK ID
    public static int LowLayerColliders = 10;
    public static int HighLayerColliders = 11;

    //Stats
    public static int MinDamage = 1;

    //Skills
    //Push logic
    public static float TimeToBlockMovePushed = 1f;
    public static float PushPowerMultiplier = 100f;
    //Caster movement logic
    public static float TimeToBlockMoveCaster = 1f;
    public static float DistanceToTarget = 0.25f;

    //UI
    //Tooltips
    public static float TimeToShowNewItemTooltip = 2f;

    //Scene
    public static string SceneMainMenu = "MainMenu";
    public static string SceneDemo = "DemoScene";
}
