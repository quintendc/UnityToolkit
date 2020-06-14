﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Player
{

    public int Id { get; private set; }
    public APawn Pawn { get; private set; }
    public APlayerController PlayerController { get; private set; }

    public Player(int id, APawn pawn, APlayerController playerController)
    {
        Id = id;
        Pawn = pawn;
        PlayerController = playerController;
    }


    /// <summary>
    /// Update Player information
    /// </summary>
    /// <param name="id">change the player Id</param>
    /// <param name="pawn">change the player Pawn</param>
    /// <param name="playerController">change the player PlayerController</param>
    public void ChangePlayer(int? id, APawn pawn = null, APlayerController playerController = null)
    {
        if (id != null)
        {
            Id = (int)id;
        }

        if (pawn != null)
        {
            Pawn = (APawn)pawn;
        }

        if (playerController != null)
        {
            PlayerController = (APlayerController)playerController;
        }
    }

}
