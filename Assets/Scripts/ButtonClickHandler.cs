using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClickHandler : MonoBehaviour
{
    private Coroutine speedBoostCoroutine;

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
            else if (int.Parse(itemValueText.text) == 2 && speedBoostCoroutine == null)
            {
                InventoryManager.instance.removeItemByButton(int.Parse(itemValueText.text));
                Destroy(gameObject);

                // Zapisz oryginaln¹ wartoœæ moveSpeed tylko raz przed pierwszym klikniêciem
                float originalMoveSpeed = PlayerController.instance.moveSpeed;

                // Zwiêksz moveSpeed o 5
                PlayerController.instance.moveSpeed += 5;

                // Uruchom coroutine, aby przywróciæ pierwotn¹ wartoœæ moveSpeed po okreœlonym czasie
                speedBoostCoroutine = StartCoroutine(RestoreMoveSpeedAfterDelay(2f, originalMoveSpeed)); // Okreœl czas trwania boosta
            }
        }
    }

    private IEnumerator RestoreMoveSpeedAfterDelay(float delay, float originalMoveSpeed)
    {
        yield return new WaitForSeconds(delay);

        // Przywróæ pierwotn¹ wartoœæ moveSpeed
        PlayerController.instance.moveSpeed = originalMoveSpeed;

        // Zresetuj referencjê do coroutine
        speedBoostCoroutine = null;
    }
}
