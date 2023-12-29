using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClickHandler : MonoBehaviour
{
    public bool speedActive = false;
    private float speedTime = 5f;
    private float originalMoveSpeed;

    private void Start()
    {
        originalMoveSpeed = PlayerController.instance.moveSpeed;
    }

    public void DestroyButtonOnClick()
    {
        Text itemValueText = transform.Find("ItemValue")?.GetComponent<Text>();

        if (itemValueText != null)
        {
            if (PlayerController.instance.health < 100 && int.Parse(itemValueText.text) == 1)
            {
                InventoryManager.instance.removeItemByButton(int.Parse(itemValueText.text));
                Destroy(gameObject);
                PlayerController.instance.health += 10;
            }
            else if (int.Parse(itemValueText.text) == 2)
            {
                InventoryManager.instance.removeItemByButton(int.Parse(itemValueText.text));
                Destroy(gameObject);

                // Zwiêksz moveSpeed o 5
                PlayerController.instance.moveSpeed += 5;
                speedActive = true;
                PlayerController.instance.speedUp();

            }
        }
    }

    private void ResetSpeed()
    {
        PlayerController.instance.moveSpeed = originalMoveSpeed;
    }
}
