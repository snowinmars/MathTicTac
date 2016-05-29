using MathTicTac.DTO;
using MathTicTac.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathTicTac.PL.RestService.Models
{
	internal static class Mapper
	{
		internal static Account AccountSMtoAccount(AccountServiceModel item)
		{
			return new Account
			{
				Draw = item.Draw,
				Id = item.Id,
				Lose = item.Lose,
				Username = item.Username,
				Won = item.Won,
			};
		}

		internal static AccountServiceModel AccounttoAccountSM(Account account)
		{
			return new AccountServiceModel(account.Username)
			{
				Draw = account.Draw,
				Id = account.Id,
				Lose = account.Lose,
				Won = account.Won,
			};
		}

		internal static CellServiceModel[,] CellArray2CellSMArray(BigCell bigcell)
		{
			CellServiceModel[,] cells = new CellServiceModel[bigcell.Cells.GetLength(0), bigcell.Cells.GetLength(1)];


			for (int e = 0; e < bigcell.Cells.GetLength(0); e++)
				for (int k = 0; k < bigcell.Cells.GetLength(1); k++)
				{
					cells[e, k] = new CellServiceModel(bigcell.Cells[e, k].State);
				}

			return cells;
		}

		internal static WorldServiceModel World2WorldSM(World world)
		{
			BigCellServiceModel[,] bigcells = Mapper.BigCellArray2BigCellSMArray(world.BigCells);

			return new WorldServiceModel(world.Id, bigcells)
			{
				Status = world.Status,
			};
		}

		internal static Move MoveSM2Move(MoveServiceModel move)
		{
			string ip = "";

			return new Move(ip , move.Token, move.GameId, new Coord(move.BigCellCoord.X, move.BigCellCoord.Y), new Coord(move.CellCoord.X, move.CellCoord.Y));
		}

		internal static BigCellServiceModel[,] BigCellArray2BigCellSMArray(BigCell[,] array)
		{
			BigCellServiceModel[,] bigcells = new BigCellServiceModel[array.GetLength(0), array.GetLength(1)];

			for (int i = 0; i < bigcells.GetLength(0); i++)
				for (int j = 0; j < bigcells.GetLength(1); j++)
				{
					CellServiceModel[,] cells = Mapper.CellArray2CellSMArray(array[i, j]);

					bigcells[i, j] = new BigCellServiceModel(array[i, j].State, array[i, j].IsFocus, cells);
				}

			return bigcells;
		}
	}
}