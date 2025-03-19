// DialogTrigger.cs
using UnityEngine;

public class DialogTrigger : MonoBehaviour 
{
    public Dialog dialog;
    public float triggerRadius = 2f;

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < triggerRadius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialog();
            }
        }
    }

    void TriggerDialog()
    {
        DialogManager.Instance.StartDialog(dialog);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}