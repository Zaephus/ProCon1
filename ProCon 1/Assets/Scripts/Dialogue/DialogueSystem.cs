using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour {

    public GameObject playerPrefab;
    private GameObject enemyPrefab;

    public Transform playerPosition;
    public Transform enemyPosition;

	public TMP_Text dialogueText;
	public List<DialogueButton> buttons = new List<DialogueButton>();
	public List<int> inputBreak = new List<int>();

	public GameObject dialogueBox;
	public GameObject optionsBox;
	public GameObject dialogueButtonPrefab;
	public GridLayoutGroup buttonGrid;
	
	private EnemyUnit currentUnit;
	public List<EnemyUnit> units = new List<EnemyUnit>();

	private DialogueOptions currentDialogueOption;

	public float delayBeforeStart = 0f;
	public float timeBetweenChars = 0.1f;
	public string leadingChar = "";
	public bool leadingCharBeforeDelay = false;

	private int index = 0;

	void Start() {

		Unit tempUnit = new Unit();
		SaveSystem.instance.LoadUnit(tempUnit,"CurrentUnit");

		foreach(EnemyUnit unit in units) {
			if(unit.unitName == tempUnit.unitName) {
				currentUnit = unit;
			}
		}

        enemyPrefab = currentUnit.unitPrefab;

        GameObject player = Instantiate(playerPrefab,playerPosition.position,Quaternion.identity,this.transform);

        GameObject enemy = Instantiate(enemyPrefab,enemyPosition.position,Quaternion.identity,this.transform);

		if(currentUnit.option == 0) {
			currentDialogueOption = currentUnit.startDialogueOption;
		}
		else if(currentUnit.option == 1) {
			currentDialogueOption = currentUnit.firstAlternateDialogueOption;
		}
		else if(currentUnit.option == 2) {
			currentDialogueOption = currentUnit.secondAlternateDialogueOption;
		}

		if(currentUnit.inAltWorld) {

			if(currentUnit.option == 3) {
				currentDialogueOption = currentUnit.firstAltWorldDialogueOption;
			}
			else if(currentUnit.option == 4) {
				currentDialogueOption = currentUnit.secondAltWorldDialogueOption;
			}

			if(currentUnit.battleDialogueOption != null) {
				currentDialogueOption = currentUnit.battleDialogueOption;
			}

		}
		else {
			if(currentUnit.option == 3) {
				currentDialogueOption = currentUnit.firstMainWorldDialogueOption;
			}
			else if(currentUnit.option == 4) {
				currentDialogueOption = currentUnit.secondMainWorldDialogueOption;
			}
		}

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
				LevelLoader.instance.StartBattle(currentUnit);
				yield return null;
			}

			if(currentDialogueOption.broodjesToGive != 0) {
				yield return new WaitForSeconds(0.5f);
				PlayerPrefs.SetInt("FrikandelBroodjes",(PlayerPrefs.GetInt("FrikandelBroodjes")+currentDialogueOption.broodjesToGive));
			}

			if(currentDialogueOption.permamentEndBreak == 1) {
				yield return new WaitForSeconds(0.5f);
				currentUnit.option = 1;
			}

			if(currentDialogueOption.permamentEndBreak == 2) {
				yield return new WaitForSeconds(0.5f);
				currentUnit.option = 2;
			}

			if(currentDialogueOption.permamentEndBreak == 3) {
				yield return new WaitForSeconds(0.5f);
				currentUnit.option = 3;
			}

			if(currentDialogueOption.permamentEndBreak == 4) {
				yield return new WaitForSeconds(0.5f);
				currentUnit.option = 4;
			}

			if(currentDialogueOption.spawnInAltWorld) {
				yield return new WaitForSeconds(0.5f);
				currentUnit.inAltWorld = true;
			}

			yield return new WaitForSeconds(0.5f);
			LevelLoader.instance.LoadLevel("LevelOneScene");
			yield return null;
		
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