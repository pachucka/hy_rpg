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

                // Zapisz oryginaln� warto�� moveSpeed tylko raz przed pierwszym klikni�ciem
                float originalMoveSpeed = PlayerController.instance.moveSpeed;

                // Zwi�ksz moveSpeed o 5
                PlayerController.instance.moveSpeed += 5;

                // Uruchom coroutine, aby przywr�ci� pierwotn� warto�� moveSpeed po okre�lonym czasie
                speedBoostCoroutine = StartCoroutine(RestoreMoveSpeedAfterDelay(2f, originalMoveSpeed)); // Okre�l czas trwania boosta
            }
        }
    }

    private IEnumerator RestoreMoveSpeedAfterDelay(float delay, float originalMoveSpeed)
    {
        yield return new WaitForSeconds(delay);

        // Przywr�� pierwotn� warto�� moveSpeed
        PlayerController.instance.moveSpeed = originalMoveSpeed;

        // Zresetuj referencj� do coroutine
        speedBoostCoroutine = null;
    }
}
