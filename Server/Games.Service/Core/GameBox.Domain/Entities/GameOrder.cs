﻿namespace GameBox.Domain.Entities;

public class GameOrder
{
    public Guid OrderId { get; set; }

    public Order Order { get; set; }

    public Guid GameId { get; set; }

    public Game Game { get; set; }
}
