using UnityEngine;

public abstract class WeaponMechanic : MonoBehaviour
{
    public GameObject firstPersonViewItem;
    public abstract void Use_Down();
    public abstract void Use_Held();
    public abstract void Use_RightDown();
    public void GiveToPlayer()
    {
        var player = Player.id;
        player.heldWeapon = this;
        firstPersonViewItem.SetActive(true);
        transform.parent = player.mCamera.transform;
        gameObject.SetActive(true);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    public void RemoveFromPlayer()
    {

        Player.id.heldWeapon = null;
        firstPersonViewItem.SetActive(false);
        transform.parent = null;
        gameObject.SetActive(false);
        ExtraRemoveCall();
    }
    public abstract void ExtraRemoveCall();
}
