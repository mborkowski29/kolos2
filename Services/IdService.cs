using kolos2.DTOs;

namespace kolos2.Services;

public interface IdService
{
    Task<CharactersDTOs> getCharacter(int id);
    Task<List<ItemsBackpacksDTOs>> AddItemsToBackpack(int characterId, List<int> itemIds);
    Task SaveChanges();
}