using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = .5f;
    [SerializeField] private WeaponInfo weaponInfo;

    private Transform weaponCollider;
    private Animator myAnimator;
    private GameObject slashAnim;
    private ActiveWeapon activeWeapon;

    private void Awake()
    {
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
 
    }


    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashSpownPoint").transform;
    }


    private void Update()
    {
        MouseFollowWithOffset();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }


    public void Attack()
    {
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }


    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }


    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

        private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        var dir = mousePos - playerScreenPoint;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 180 - angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}