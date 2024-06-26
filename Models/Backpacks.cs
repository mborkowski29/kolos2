﻿using System.ComponentModel.DataAnnotations.Schema;

namespace kolos2.Models;

public class Backpacks
{
    public int CharacterId { get; set; }
    
    public int ItemId { get; set; }
    public int Amount { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    
    public Characters Characters { get; set; }
    [ForeignKey(nameof(ItemId))]
    
    public Items Items { get; set; }
}