using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnim : MonoBehaviour {


    private Move thePlayer;
    private Animator Wanimator;

    public static int weapon_none = 0; //
    public static int weapon_LongSword = 1; //
    public static int weapon_Spear = 2; //
    public int _currentWeapon = weapon_none;

    public void changeWeapon(int weapon)
    {
        if (_currentWeapon == weapon)
        {
            return;
        }
        else
        {
            Wanimator.SetInteger("weapon", weapon);
            _currentWeapon = weapon;
        }
    }

    // Use this for initialization
    void Start () {
        Wanimator = GetComponent<Animator>();
        thePlayer = FindObjectOfType<Move>();
    }
	
	// Update is called once per frame
	void Update () {
        if (thePlayer._currentAnimationState == 3)
        {
            changeWeapon(weapon_LongSword);
        } else
        {
            changeWeapon(weapon_none);
        }
        
        //if (thePlayer.GetComponent<SpriteRenderer>().flipX == true)
        //{
        //    this.transform.position = new Vector3(thePlayer.transform.position.x-0.7f, this.transform.position.y, this.transform.position.z);
        //    this.transform.rotation = new Quaternion(0.00f, 0.00f, 1f, 0.00f);
        //}
        //else
        //{
        //    this.transform.position = new Vector3(thePlayer.transform.position.x+0.7f, this.transform.position.y, this.transform.position.z);
        //    this.transform.rotation = new Quaternion(0.00f, 0.00f, 1f, 100.00f);
        //}
    }
}
