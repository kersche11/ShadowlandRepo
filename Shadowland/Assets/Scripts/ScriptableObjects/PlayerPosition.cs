using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

[CreateAssetMenu]
public class PlayerPosition : ScriptableObject
{
    [SerializeField] 
    private Vector3 _playerPos;
    [SerializeField]
    private Quaternion _playerRot;




    public Vector3 PlayerPos { get => _playerPos; set => _playerPos = value; }
    public Quaternion PlayerRot { get => _playerRot; set => _playerRot = value; }

}
//Dungeon1
//UnityEditor.TransformWorldPlacementJSON:
//{ "position":{ "x":-129.4240264892578,"y":67.33999633789063,"z":179.2743377685547},
//"rotation":{ "x":0.0,"y":-0.507566511631012,"z":0.0,"w":0.8616126179695129},
//"scale":{ "x":1.0,"y":1.0,"z":1.0} }

//Dungeon2
//UnityEditor.TransformWorldPlacementJSON:{
//"position":{ "x":54.985001525878909,"y":29.867000579833986,"z":486.2220129394531},
//"rotation":{ "x":-0.00034494156716391444,"y":-0.5496936440467835,"z":-0.0014873656909912825,"w":0.2308742254972458},
//"scale":{ "x":1.0,"y":1.0,"z":1.0} }

//Dungeon3
//UnityEditor.TransformWorldPlacementJSON:{
//"position":{ "x":208.73999633789063,"y":55.524034729003909,"z":-151.21000061035157},
//"rotation":{ "x":0.0,"y"0.94,"z":0.0,"w":0.6894466280937195},
//"scale":{ "x":1.0,"y":1.0,"z":1.0} }

