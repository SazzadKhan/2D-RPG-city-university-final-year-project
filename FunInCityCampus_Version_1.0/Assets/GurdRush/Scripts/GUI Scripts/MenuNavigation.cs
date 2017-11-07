using UnityEngine;
using System.Collections;

public class MenuNavigation : MonoBehaviour {


	public void MainMenu()
	{	// Load Menu scene
		Application.LoadLevel("menu");
	}

	public void Quit()
	{	// quite application
		Application.Quit();
	}
	
	public void Play()
	{	
		// Load main game
		Application.LoadLevel("prototyping");
	}
	
	public void HighScores()
	{
		Application.LoadLevel("scores");
		
	}
	public void SubMission(){
		// load the Sub game scene
		Application.LoadLevel("game");
	}

    public void Credits()
    {	// load credit
        Application.LoadLevel("credits");
    }

	public void Help()
	{	// load help
		Application.LoadLevel("Help");
	}

	public void SourceCode()
	{
		Application.OpenURL("https://github.com/vilbeyli/Pacman-Clone/");
	}
}
