using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
  [SerializeField] private int currentID;
  [SerializeField] private int currentLevel;
  [SerializeField] private GameObject bullets;
  [SerializeField] private int speedBullets;
  [SerializeField] private float rangeAttack;

  public WeaponType weaponType;

  public enum WeaponType {
    Smg,
    Piston
  }

  public WeaponState weaponState;

  public enum WeaponState {
    Idle,
    Attack
  }

  private void Update() {
    switch (weaponState) {
      case WeaponState.Idle:
        Idle();
        break;
      case WeaponState.Attack:
        Attack();
        break;
    }
  }

  private void Idle() {
  }

  private void Attack() {
  }
}