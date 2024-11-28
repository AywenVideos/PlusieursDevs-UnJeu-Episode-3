using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using Saidus2;
using TMPro;
using UnityEngine;

[System.Serializable]
public class VoiceoverEntry
{
    public string dialogueKey; // Format: "CharacterName:DialogueLine"
    public AudioClip audioClip;
}


// How to add a voiceover to your dialogue (by ri1_)
// 1. Record the voiceover
// 2. Place it in the Dialogues/Voices/[CHARACTER_NAME] folder
// 3. Add a new voiceover entry (in this script's settings) with the dialogue key like this : [CHARACTER_NAME (as it is in the dialogue, case sensitive)]:[STRING] and the audio_ clip you recorded
// 4. It SHOULD work.

public class DialoguesManager : MonoBehaviour
{
    [SerializeField] PlayerControls playerControls;
    [SerializeField] AudioSource dialogueAudioSource;

    [SerializeField] GameObject dialoguesPanel;
    [SerializeField] GameObject valuesPanel;
    [SerializeField] float dialogueDisplayCharacterSpeed;
    [SerializeField] TMP_Text authorText;
    [SerializeField] TMP_Text messageText;
    [SerializeField] AudioClip defaultVoiceoverClip;

    public static string PlayerName = "T'as oublié de mettre un nom :p";

    private static DialoguesManager instance;
    public static DialoguesManager Instance
    {
        get { return instance; }
    }

    [SerializeField] private List<VoiceoverEntry> voiceoverEntries = new List<VoiceoverEntry>();

    private Dictionary<string, AudioClip> voiceoverClips = new Dictionary<string, AudioClip>();

    Coroutine coroutine_Dialogues;
    Story currentDialogues;
    bool isMousePressed;

    private void Awake()
    {
        instance = this;
        currentDialogues = null;
        dialogueAudioSource.Stop();
        dialoguesPanel.SetActive(false);
        valuesPanel.SetActive(true);

        // Populate dictionary from list of voiceover entries
        foreach (var entry in voiceoverEntries)
        {
            if (!voiceoverClips.ContainsKey(entry.dialogueKey) && entry.audioClip != null)
            {
                voiceoverClips.Add(entry.dialogueKey, entry.audioClip);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isMousePressed)
            {
                isMousePressed = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
        }
    }

    public void BeginDialogues(TextAsset dialogues)
    {
        StopAllDialoguesAndFreeze();
        StartDialogues(dialogues);
    }

    public void BeginDialogues(TextAsset dialogues, Vector3 lookAt)
    {
        StopAllDialoguesAndFreeze();
        playerControls.ForcePlayerToLookAt(lookAt, 2f);
        StartDialogues(dialogues);
    }

    private void StopAllDialoguesAndFreeze()
    {
        dialoguesPanel.SetActive(false);
        if (coroutine_Dialogues != null) StopCoroutine(coroutine_Dialogues);
    }

    private void StartDialogues(TextAsset dialogues)
    {
        coroutine_Dialogues = StartCoroutine(StartDialogues_Coroutine(dialogues));
    }

    IEnumerator StartDialogues_Coroutine(TextAsset dialogues)
    {
        currentDialogues = new Story(dialogues.text);
        playerControls.Freeze = true;

        while (currentDialogues.canContinue)
        {
            string dialogueLine = currentDialogues.Continue().Trim();

            if (dialogueLine.StartsWith("FORCE_LOOK_AT"))
            {
                string[] commandParams = dialogueLine.Split(' ');
                if (commandParams.Length == 4)
                {
                    float x = float.Parse(commandParams[1]);
                    float y = float.Parse(commandParams[2]);
                    float z = float.Parse(commandParams[3]);

                    Vector3 lookAtPosition = new Vector3(x, y, z);
                    playerControls.ForcePlayerToLookAt(lookAtPosition, 2.0f);
                }
            }
            else if (dialogueLine.StartsWith("WAIT"))
            {
                dialoguesPanel.SetActive(false);

                string[] commandParams = dialogueLine.Split(' ');

                if (commandParams.Length == 2)
                {
                    if (float.TryParse(commandParams[1], out float result))
                    {
                        yield return new WaitForSeconds(result);
                    }
                }
            }
            else if (dialogueLine.StartsWith("TRIGGER"))
            {
                dialoguesPanel.SetActive(false);

                string[] commandParams = dialogueLine.Split(' ');

                if (commandParams.Length == 2)
                {
                    yield return TriggerEvent_Coroutine(commandParams[1]);
                }
            }
            else if (dialogueLine.StartsWith("OPEN_OBJ"))
            {
                string[] commandParams = dialogueLine.Split(' ');

                if (commandParams.Length >= 2)
                {
                    ObjectivesManager.Instance.OpenObjectives(string.Join(' ', commandParams[1..]));
                }
            }
            else
            {
                string[] splitLine = dialogueLine.Split(':');

                if (splitLine.Length == 2)
                {
                    string characterName = splitLine[0].Trim();
                    string dialogue = splitLine[1].Trim();

                    if (characterName == "PLAYER_NAME") characterName = PlayerName;

                    yield return StartCoroutine(DisplayDialogueAndWait(characterName, dialogue));
                }
            }
            yield return null;
        }

        playerControls.Freeze = false;
        dialoguesPanel.SetActive(false);
        valuesPanel.SetActive(true);
    }

    IEnumerator TriggerEvent_Coroutine(string eventName)
    {
        print($"event trigger : {eventName}");
        if (eventName == "emergency_call")
        {
            float time = Random.Range(1f, 5f);
            EmergencyCall.Instance.Bip(time);
            yield return new WaitForSeconds(time);
        }
        else if (eventName == "pick_up")
        {
            EmergencyCall.Instance.PickUp();
            yield return null;
        }
        else if (eventName == "screamer")
        {
            EmergencyCall.Instance.Screamer();
            yield return null;
        }
        else if (eventName == "awake")
        {
            Bed.instance.TriggerAwake();
            yield return null;
        }
    }

    IEnumerator DisplayDialogueAndWait(string name, string message)
    {
        dialoguesPanel.SetActive(true);
        valuesPanel.SetActive(false);

        string dialogueKey = $"{(name == PlayerName ? "PLAYER" : name)}:{message}";
        print(dialogueKey);

        // Check if a specific voiceover exists for this dialogue line
        if (voiceoverClips.TryGetValue(dialogueKey, out AudioClip voiceoverClip))
        {
            dialogueAudioSource.clip = voiceoverClip;
            dialogueAudioSource.loop = false;  // Ensure voiceover does not loop
            dialogueAudioSource.Play();
        }
        else
        {
            print($"No voicerover for {dialogueKey}");
            dialogueAudioSource.clip = defaultVoiceoverClip;
            dialogueAudioSource.loop = false;  // Ensure default sound does not loop
            dialogueAudioSource.Play();
        }

        authorText.text = name;
        messageText.text = "";
        string displayedMessage = "";

        // Display the message character by character
        for (int i = 0; i < message.Length; i++)
        {
            if (isMousePressed)
            {
                // Skip to the end of the message if clicked
                displayedMessage += message.Substring(i);
                messageText.text = displayedMessage;
                i = message.Length;

                // Only stop the audio_ if skipping the line
                if (dialogueAudioSource.isPlaying)
                {
                    dialogueAudioSource.Stop();
                }

                while (isMousePressed)
                {
                    yield return new WaitForEndOfFrame();
                }
                break;
            }

            displayedMessage += message[i];
            messageText.text = displayedMessage;
            yield return new WaitForSeconds(dialogueDisplayCharacterSpeed);
        }

        // Wait until the user clicks to advance
        messageText.text = message;
        yield return new WaitUntil(() => Input.GetMouseButton(0));

        // Ensure audio_ stops if it hasn't already finished
        if (dialogueAudioSource.isPlaying)
        {
            dialogueAudioSource.Stop();
        }

        // Prevent accidental continuous advancement
        while (isMousePressed)
        {
            yield return new WaitForEndOfFrame();
        }
    }

}
