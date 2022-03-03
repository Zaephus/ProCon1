using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _tmpProText;
    //[SerializeField] private List<string> dialogueList = new List<string>();
    [SerializeField] private List<TextMeshProUGUI> buttonTexts = new List<TextMeshProUGUI>();
    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private List<int> inputBreak = new List<int>();
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject optionsBox;
    [SerializeField] private DialogueOptions currentDialogueOptions;
    string writer;

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;

    private int index = 0;
    private int inputIndex = 0;

    void Start()
    {
        if (_tmpProText != null)
        {
            writer = _tmpProText.text;
        }
        for (int i = 0; i < currentDialogueOptions.nextDialogueOptions.Count; i++)
        {
            Debug.Log(currentDialogueOptions.nextDialogueOptions[i].nextDialogueOptions.Count);
        }
        AddNewDialogueOptions(currentDialogueOptions);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (index == inputBreak[inputIndex])
            {
                _tmpProText.text = "doe even alsof hier nu knoppen staan oke";
            }
            else
            {
                index++;
                Debug.Log("index: " + index + " lengte: " + currentDialogueOptions.dialogueList.Count);
                if (index == currentDialogueOptions.dialogueList.Count)
                {
                    Debug.Log("AAAAH" + index);
                    index = 0;
                    dialogueBox.SetActive(false);
                }
                else
                {
                    StartDialogue();
                }
            }
        }
    }

    public void StartDialogue()
    {
        if (index == inputBreak[inputIndex])
        {
            optionsBox.SetActive(true);
            for (int i = 0; i < buttonTexts.Count; i++)
            {
                buttonTexts[i].text = currentDialogueOptions.responseOptions[i];
            }
        }
        else
        {
            optionsBox.SetActive(false);
        }
        dialogueBox.SetActive(true);
        _tmpProText.text = "";
        StartCoroutine(TypeWriterTMP(currentDialogueOptions.dialogueList[index]));
    }

    IEnumerator TypeWriterTMP(string _currentDialogue)
    {
        _tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        int _thisIndex = index;
        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in _currentDialogue)
        {
            if (index != _thisIndex)
            {
                yield break;
            }
            if (_tmpProText.text.Length > 0)
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
            }
            _tmpProText.text += c;
            _tmpProText.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        yield return new WaitForSeconds(0.35f);
        _tmpProText.text += "\npress space to continue";

        if (leadingChar != "")
        {
            _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
        }
    }

    public void AddNewDialogueOptions(DialogueOptions _newDialogueOptions)
    {
        currentDialogueOptions = _newDialogueOptions;
        index = 0;
        //Debug.Log(currentDialogueOptions.nextDialogueOptions.Count + " " + buttons.Count);
        Debug.Log(currentDialogueOptions);


        for (int i = 0; i < buttons.Count; i++)
        {
            Debug.Log("aaaah" + i + " " + currentDialogueOptions.nextDialogueOptions.Count + " " + buttons.Count);
            buttons[i].onClick.RemoveAllListeners();
            int x = i;
            buttons[i].onClick.AddListener(() => this.AddNewDialogueOptions(currentDialogueOptions.nextDialogueOptions[x]));
            //buttons[i].onClick.AddListener(delegate { this.AddNewDialogueOptions(currentDialogueOptions.nextDialogueOptions[i]); });
            //buttons[i].onClick.AddListener(() => GeenIdee());
        }
        optionsBox.SetActive(false);
        StartDialogue();
    }

    public void GeenIdee()
    {
        Debug.Log("warom de kanker werk ik niet");
    }
}
