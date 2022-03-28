using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour {

	public TMP_Text dialogueText;
	public List<DialogueButton> buttons = new List<DialogueButton>();
	public List<int> inputBreak = new List<int>();

	public GameObject dialogueBox;
	public GameObject optionsBox;
	public GameObject dialogueButtonPrefab;
	public GridLayoutGroup buttonGrid;
	public EnemyUnit enemyUnit;

	private DialogueOptions currentDialogueOption;

	public float delayBeforeStart = 0f;
	public float timeBetweenChars = 0.1f;
	public string leadingChar = "";
	public bool leadingCharBeforeDelay = false;

	private int index = 0;

	void Start() {

		currentDialogueOption = enemyUnit.startDialogueOption;
		AddNewDialogueOptions(currentDialogueOption);

		foreach(DialogueButton b in buttons) {
			b.ActivePercentages(false);
		}

	}

	private void Update() {

		if(Input.GetKeyDown(KeyCode.Space) && index != currentDialogueOption.inputBreak) {

			index++;
			if(index == currentDialogueOption.dialogueList.Count) {
				index = 0;
				dialogueBox.SetActive(false);
			}
			else {
				StartDialogue();
			}

		}

	}

	public void StartDialogue() {

		if (index == currentDialogueOption.inputBreak) {

			for(int i = 0; i < currentDialogueOption.responseOptions.Count; i++) {

				GameObject b = Instantiate(dialogueButtonPrefab,buttonGrid.transform.position,Quaternion.identity,optionsBox.transform);

				b.transform.SetParent(buttonGrid.transform,false);
				b.transform.localScale = new Vector3(1,1,1);
         		b.transform.localPosition = Vector3.zero;

				buttons.Add(b.GetComponent<DialogueButton>());
				
				SetPercentages();

			}

			AddListeners();

			optionsBox.SetActive(true);

			for(int i = 0; i < buttons.Count; i++) {
				buttons[i].ActivePercentages(false);
				buttons[i].SetButtonText((i+1) + ". " + currentDialogueOption.responseOptions[i]);
			}

		}
		else {
			optionsBox.SetActive(false);
		}

		dialogueBox.SetActive(true);
		dialogueText.text = "";

		StartCoroutine(TypeWriter(currentDialogueOption.dialogueList[index]));

	}

	IEnumerator TypeWriter(string _currentDialogue) {

		dialogueText.text = leadingCharBeforeDelay ? leadingChar : "";

		int _thisIndex = index;
		yield return new WaitForSeconds(delayBeforeStart);

		foreach(DialogueButton b in buttons) {
			b.gameObject.SetActive(false);
		}

		foreach(char c in _currentDialogue) {

			if(index != _thisIndex) {
				yield break;
			}

			if(dialogueText.text.Length > 0) {
				dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - leadingChar.Length);
			}

			dialogueText.text += c;
			dialogueText.text += leadingChar;

			yield return new WaitForSeconds(timeBetweenChars);

		}

		yield return new WaitForSeconds(0.35f);

		foreach(DialogueButton b in buttons) {
			b.gameObject.SetActive(true);
		}

		if(index != currentDialogueOption.inputBreak) {
			dialogueText.text += "\npress space to continue";
		}

		if(leadingChar != "") {
			dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - leadingChar.Length);
		}

		if(index == currentDialogueOption.endBreak) {

			if(index == currentDialogueOption.combatBreak) {
				yield return new WaitForSeconds(0.5f);
				LevelLoader.instance.LoadLevel("BattleScene");
				yield return null;
			}
			else {
				yield return new WaitForSeconds(0.5f);
				LevelLoader.instance.LoadLevel("LevelOneScene");
				yield return null;
			}
		
		}

	}

	public void AddNewDialogueOptions(DialogueOptions _newDialogueOptions) {

		SetPercentages();

		foreach(DialogueButton b in buttons) {
			b.ActivePercentages(true);
		}

		currentDialogueOption = _newDialogueOptions;
		index = 0;

		SaveSystem.instance.LoadDialogueOption(currentDialogueOption,currentDialogueOption.name);
		Debug.Log(currentDialogueOption);

		StartCoroutine(SetPercentageDelay());

	}

	public void AddListeners() {

		List<Button> buttonUnits = new List<Button>();

		foreach(DialogueButton b in buttons) {
			buttonUnits.Add(b.GetComponent<Button>());
		}

		for(int i = 0; i < buttonUnits.Count; i++) {

			buttonUnits[i].onClick.RemoveAllListeners();

			int x = i;

			buttonUnits[i].onClick.AddListener(() => this.AddChosenCount(x));
			buttonUnits[i].onClick.AddListener(() => this.AddNewDialogueOptions(currentDialogueOption.nextDialogueOptions[x]));

		}

	}

	public IEnumerator SetPercentageDelay() {
		
		yield return new WaitForSeconds(3f);
		optionsBox.SetActive(false);

		for(int i = buttons.Count-1; i >= 0; i--) {
			Destroy(buttons[i].gameObject);
			buttons.RemoveAt(i);
		}

		StartDialogue();

	}

	private void SetPercentages() {

		float total = 0;
		List<float> amounts = new List<float>();

		for(int i = 0; i < currentDialogueOption.chosenAmount.Length; i++) {
			total += currentDialogueOption.chosenAmount[i];
		}

		for(int i = 0; i < currentDialogueOption.chosenAmount.Length; i++) {
			amounts.Add((currentDialogueOption.chosenAmount[i]/total)*100);
		}

		for(int i = 0; i < buttons.Count; i++) {
			buttons[i].SetPercentageText(amounts[i] + "%");
			buttons[i].SetPercentageText(string.Format("{0:0.00}", amounts[i]) + "%");
		}

	}

	public void AddChosenCount(int _index) {
		currentDialogueOption.chosenAmount[_index]++;
		SaveSystem.instance.SaveDialogueOption(currentDialogueOption,currentDialogueOption.name);
	}
}