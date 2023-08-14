using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StaticProperty", order = 1)]
public class StaticProperty : ScriptableObject
{
    public static bool isVisible;
    public static int jumps;
    public static bool grounded;
    public static Vector2 speed;
    public static float defaultGravity;
    public static float fallingGravity;
    public static float jumpForce;
    public static float camerasLeftEdgePos;
    public static Vector2 screenSize;
    public static bool canDoubleJump;
    public static Collider2D thereIsATileDown;
    public static Collider2D thereIsATileUp;
    public static int sideTiles;
    public static float deadlineDown;
    public static float deadlineUp;
    public static int score;
}
