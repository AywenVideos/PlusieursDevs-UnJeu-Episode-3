using UnityEngine;
using Saidus2;

public class Bed : Interactable
{
    public static Bed instance;

    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private GameObject fadeCanvas;

    [SerializeField] private TextAsset dialogue;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform door;

    private bool isEnabled = false;

    public override void Interact()
    {
        if (isEnabled) return;
        isEnabled = true;

        base.Interact();

        instance = this;
        
        //playerControls.Freeze = true;
        fadeCanvas.SetActive(true);

        DialoguesManager.Instance.BeginDialogues(dialogue);
    }

    public void TriggerAwake()
    {
        foreach (Light light in FindObjectsOfType<Light>())
        {
            if (!light.CompareTag("PlayerLight"))
            {
                light.enabled = false;
            }
        }

        foreach (GlitchTv tv in FindObjectsOfType<GlitchTv>())
        {
            tv.ON = false;
        }

        door.rotation = new Quaternion(0, 0.165365741f, 0, 0.98623234f);

        animator.SetTrigger("Awake");
        ObjectivesManager.Instance.OpenObjectives("Explorer la maison");
        Destroy(gameObject);
    }
}
