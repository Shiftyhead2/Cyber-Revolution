using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class SettingsManager : MonoBehaviour {


	private static SettingsManager _instance;
	public GameObject CanvasGroup;
	public GameObject MainCanvas;
	public GameObject fullscreenToggle;
	public GameObject resolutionDropdown;
	public GameObject textureQualityDropdown;
	public GameObject antialiasingDropdown;
	public GameObject vSyncDropdown;
	public GameObject ExitButton;

	public GameObject ApplyButton;

	public Resolution[] resolutions;
	public GameSettings gameSettings;

	void Start(){
		if (!_instance) {
			_instance = this;
		} else {
			Destroy (this.gameObject);
		}
		MainCanvas = GameObject.Find("Settings");
		CanvasGroup = GameObject.Find("Main Menu");
		fullscreenToggle = GameObject.Find ("Toggle");
		resolutionDropdown = GameObject.Find ("Resolution DropDown");
		textureQualityDropdown = GameObject.Find ("Texture DropDown");
		antialiasingDropdown = GameObject.Find ("AntiAliasing DropDown");
		vSyncDropdown = GameObject.Find ("VSync");
		ExitButton = GameObject.Find ("Exit Button");
		ApplyButton = GameObject.Find ("Apply");
		fullscreenToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate {OnFullscreenToggle();});
		textureQualityDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate {OnTextureQualityChange();});
		antialiasingDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate {OnAntialiasingChange();});
		vSyncDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate {OnvSyncChange();});
		ApplyButton.GetComponent<Button>().onClick.AddListener(delegate {OnApplyButtonClick();});
		ExitButton.GetComponent<Button>().onClick.AddListener (delegate {OnExitButtonClick();});
		resolutionDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate {OnResolutionChange();});

		resolutions = Screen.resolutions;
		foreach (Resolution resolution in resolutions) {
			resolutionDropdown.GetComponent<Dropdown>().options.Add (new Dropdown.OptionData (resolution.ToString ()));
		}


		if (File.Exists (Application.persistentDataPath + "/gameSettings.json") == true) {
			LoadSettings ();
		}


		DontDestroyOnLoad (this.gameObject);
	}

	void Update(){
		MainCanvas = GameObject.Find("Settings");
		if (MainCanvas != null) {
			CanvasGroup = GameObject.Find("Main Menu");
			fullscreenToggle = GameObject.Find ("Toggle");
			resolutionDropdown = GameObject.Find ("Resolution DropDown");
			textureQualityDropdown = GameObject.Find ("Texture DropDown");
			antialiasingDropdown = GameObject.Find ("AntiAliasing DropDown");
			vSyncDropdown = GameObject.Find ("VSync");
			ExitButton = GameObject.Find ("Exit Button");
			ApplyButton = GameObject.Find ("Apply");
			fullscreenToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate {OnFullscreenToggle();});
			textureQualityDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate {OnTextureQualityChange();});
			antialiasingDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate {OnAntialiasingChange();});
			vSyncDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate {OnvSyncChange();});
			ApplyButton.GetComponent<Button>().onClick.AddListener(delegate {OnApplyButtonClick();});
			ExitButton.GetComponent<Button>().onClick.AddListener (delegate {OnExitButtonClick();});
			resolutionDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate {OnResolutionChange();});
		}else if(MainCanvas == null){
			//Do nothing
		}


	}



	void OnEnable(){
		gameSettings = new GameSettings ();


	}

	public void OnFullscreenToggle(){
		gameSettings.fullScreen = Screen.fullScreen = fullscreenToggle.GetComponent<Toggle>().isOn;
	}

	public void OnResolutionChange(){
		Screen.SetResolution (resolutions [resolutionDropdown.GetComponent<Dropdown>().value].width, resolutions [resolutionDropdown.GetComponent<Dropdown>().value].height, Screen.fullScreen);
		gameSettings.resolutionIndex = resolutionDropdown.GetComponent<Dropdown>().value;

	}

	public void OnTextureQualityChange(){
		QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.GetComponent<Dropdown>().value;

	}

	public void OnAntialiasingChange(){
		QualitySettings.antiAliasing = gameSettings.antiAliasing= (int)Mathf.Pow (2, antialiasingDropdown.GetComponent<Dropdown>().value);

	}

	public void OnvSyncChange(){
		QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.GetComponent<Dropdown>().value;

	}

	public void OnApplyButtonClick(){
		SaveSettings ();
		OnExitButtonClick ();
	}

	public void OnExitButtonClick(){
		SaveSettings ();
		MainCanvas.GetComponentInChildren<Canvas> ().enabled = false;
		CanvasGroup.GetComponent<MenuBehaviour> ().IsInSettings = false;
	}

	public void SaveSettings(){
		string jsonData = JsonUtility.ToJson (gameSettings,true);
		File.WriteAllText (Application.persistentDataPath + "/gameSettings.json", jsonData);

		
	}

	public void LoadSettings(){
		
		gameSettings = JsonUtility.FromJson<GameSettings> (File.ReadAllText(Application.persistentDataPath + "/gameSettings.json"));

		antialiasingDropdown.GetComponent<Dropdown>().value = gameSettings.antiAliasing;
		vSyncDropdown.GetComponent<Dropdown>().value = gameSettings.vSync;
		textureQualityDropdown.GetComponent<Dropdown>().value = gameSettings.textureQuality;
		resolutionDropdown.GetComponent<Dropdown>().value = gameSettings.resolutionIndex;
		fullscreenToggle.GetComponent<Toggle>().isOn = gameSettings.fullScreen;

		Screen.fullScreen = fullscreenToggle.GetComponent<Toggle>().isOn;

		resolutionDropdown.GetComponent<Dropdown>().RefreshShownValue ();
		
	}


}
