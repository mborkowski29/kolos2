


using kolos2.DTOs;
using kolos2.Models;
using Microsoft.EntityFrameworkCore;

namespace kolos2.Services
{
    public class Service : IdService
    {
        private readonly DBContext _dbContext;

        public Service(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CharactersDTOs> getCharacter(int id)
        {
            var charact = await _dbContext.Characters
                .Include(chara => chara.Backpacks)
                     .ThenInclude(backp => backp.Items)
                .Include(chara => chara.Titles)
                     .ThenInclude(ct => ct.titles)
                .FirstOrDefaultAsync(chara => chara.Id == id);

            if (charact == null)
            {
                return null;
            }

            return new CharactersDTOs
            {
                firstName = charact.FirstName,
                lastName = charact.LastName,
                currentWeight = charact.CurrentWeight,
                maxWeight = charact.MaxWeight,
                backpacksItems = charact.Backpacks.Select(backp => new BackpacksItemsDTOs
                {
                    itemName = backp.Items.Name,
                    itemWeight = backp.Items.Weight,
                    amount = backp.Amount
                }).ToList(),
                titles = charact.Titles.Select(charat => new TitlesDTOs
                {
                    title = charat.titles.Name,
                    aquiredAt = charat.AcquiredAt
                }).ToList()
            };
        }

        public async Task<List<ItemsBackpacksDTOs>> AddItemsToBackpack(int characterId, List<int> itemIds)
        {
            var characters = await _dbContext.Characters
                .Include(chara => chara.Backpacks)
                     .ThenInclude(backp=>backp.Items)
                .FirstOrDefaultAsync(chara => chara.Id == characterId);

            if (characters == null)
                throw new Exception("nie udalo sie znalezc charakteru");

            var itemsToAdd = await _dbContext.Items.Where(item => itemIds.Contains(item.Id)).ToListAsync();

            if (itemsToAdd.Count != itemIds.Count)
                throw new Exception("brak tego itemu");

            int totalWeight = itemsToAdd.Sum(item => item.Weight);
            if (characters.CurrentWeight + totalWeight > characters.MaxWeight)
                throw new Exception("itemy sa za duze");

            foreach (var item in itemsToAdd)
            {
                var backpacksItems = characters.Backpacks.FirstOrDefault(backp => backp.ItemId == item.Id);
                if (backpacksItems != null)
                {
                    backpacksItems.Amount++;
                }
                else
                {
                    characters.Backpacks.Add(new Backpacks
                    {
                        ItemId = item.Id,
                        CharacterId = characterId,
                        Amount = 1
                    });
                }
            }

            characters.CurrentWeight += totalWeight;
            await SaveChanges();

           return characters.Backpacks.Select(backp => new ItemsBackpacksDTOs()
            {
                itemId = backp.Items.Id,
                characterId = backp.CharacterId,
                amount = backp.Amount
            }).ToList();
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}