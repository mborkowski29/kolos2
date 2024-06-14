namespace kolos2.DTOs;

public class CharactersDTOs
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public int currentWeight { get; set; }
    public int maxWeight { get; set; }
    public List<BackpacksItemsDTOs> backpacksItems { get; set; }
    public List<TitlesDTOs> titles { get; set; }
}