using UnityEngine;
using System;

[System.Serializable]
public class Profile
{
	// Player score
	public int score;
	public string username = "";
	
	// Static
	public static Profile instance;


	public Profile()
	{
		
	}

	public static void Save( string nomDuProfile )
	{
		// On utilise la classe StateStorage pour sérialiser notre object en string et l'enregistrer dans les playerPrefs
		StateStorage.SaveData<Profile>(nomDuProfile, instance);
	}

	public static Profile Load( string nomDuProfile)
	{
		// Si c'est notre premier chargement dans le jeu
		
		// On essai de charger le profil depuis les playerPrefs
		Profile data = StateStorage.LoadData<Profile>(nomDuProfile);
			
		// Si le projet n'a jamais encore été enregistré dans les playerPrefs, on crée une nouvelle instance de cette classe
		if( data == null )
			data = new Profile();
		instance = data;	
		
		return instance;
	}
}