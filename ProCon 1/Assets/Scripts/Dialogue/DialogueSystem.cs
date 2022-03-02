using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
	[SerializeField] private TMP_Text _tmpProText;
	[SerializeField] private List<string> dialogueList = new List<string>();
	[SerializeField] private List<int> inputBreak = new List<int>(); 
	[SerializeField] private GameObject dialogueBox;
	string writer;

	[SerializeField] float delayBeforeStart = 0f;
	[SerializeField] float timeBtwChars = 0.1f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;

	private int index = -1;
	private int inputIndex = 0;

	void Start()
	{
		if (_tmpProText != null)
		{
			writer = _tmpProText.text;
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(index == inputBreak[inputIndex])
			{
				_tmpProText.text = "doe even alsof hier nu knoppen staan oke";
			}
			else
			{
				index++;
				Debug.Log("index: " + index + " lengte: " + dialogueList.Count);
				if (index == dialogueList.Count)
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
		dialogueBox.SetActive(true);
		_tmpProText.text = "";
		StartCoroutine(TypeWriterTMP(dialogueList[index]));
	}

	IEnumerator TypeWriterTMP(string _currentDialogue)
	{
		_tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

		int _thisIndex = index;
		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in _currentDialogue)
		{
			if(index != _thisIndex)
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
}
