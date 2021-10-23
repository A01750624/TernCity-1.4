
[System.Serializable]

// Naming: YEEET.Jugador jugadorDato = JsonConvert.DeserializeObject<YEEET.Jugador>(reJson.Substring(1, reJson.Length-2));
public class DBResponse {

  [System.Serializable]
  public class Structure
  {
    public string name;
    public string description;
    public int level;
    public int resources;
    public int cost;
  }

  [System.Serializable]
  public class BoughtStructure
  {
    public string name;
    public bool status;
  }

  [System.Serializable]
  public class Player
  {
    public string playerName;
    public int level;
    public int avatar;
    public int steel;
    public int secretWord;
    public int secretWordProgress;
    public int stars;

  }

  [System.Serializable]
  public class Missions
  {
    public string key;
    public string name;

    // ** Theory
    public int stars;

    // ** Delivery
    public int steel;
    public int percentage;
  }

  [System.Serializable]
  public class Question
  {
    public string question;
    public int questionID;
  }

  [System.Serializable]
  public class Answer
  {
    public string answer;
    public bool status;
  }

  [System.Serializable]
  public class TotalAvatar
  {
    public int avatarIndex;
  }

  // * Special mission
  [System.Serializable]
  public class SpecialStructure
  {
    public bool status;
  }

  [System.Serializable]
  public class SpecialMission
  {
    public int coins;
  }

}