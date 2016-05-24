using MathTicTak.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathTicTac.Entities;
using MathTicTac.Logic.Additional;
using System.Security.Authentication;

namespace MathTicTac.Logic
{
    public class GameLogic : IGameLogic
    {
        private IAccountDao accDao;
        private IGameDao gameDao;

        public GameLogic(IGameDao gameDao ,IAccountDao accDao)
        {
            if (accDao == null || gameDao == null)
            {
                throw new ArgumentNullException();
            }

            this.accDao = accDao;
            this.gameDao = gameDao;
        }

        public bool CreateNew(string player1Token, string player1Ip, string player2Identifier)
        {
            if (string.IsNullOrWhiteSpace(player1Token) || string.IsNullOrWhiteSpace(player2Identifier))
            {
                throw new ArgumentNullException();
            }

            if (Security.TokenIpPairIsValid(player1Token, player1Ip, accDao))
            {
                int player1Id = accDao.GetUserIdByToken(player1Token);

                int player2Id = accDao.GetUserIdByIdentifier(player2Identifier);

                return gameDao.Add(player1Id, player2Id);
            }

            throw new AuthenticationException();
        }

        public IEnumerable<GameInfo> GetAllActiveGames(string token, string ip)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException();
            }

            if (Security.TokenIpPairIsValid(token, ip, accDao))
            {
                int userId = accDao.GetUserIdByToken(token);

                return gameDao.GetAllActiveGames(userId);
            }

            throw new AuthenticationException();
        }

        public World GetCurrentWorld(string token, string ip, int gameId)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException();
            }

            if (Security.TokenIpPairIsValid(token, ip, accDao))
            {
                int userId = accDao.GetUserIdByToken(token);

                World currentGame = gameDao.GetGameState(gameId);

                int curUserState = gameDao.GetGameRole(currentGame.Id, userId);

                if (curUserState == -1)
                {
                    Mechanic.InvertStates(currentGame);

                    if (currentGame.Status != Entities.Enum.GameStatus.Active)
                    {
                        if (Mechanic.GetNumberOfMadeMoves(currentGame) % 2 == 0)
                        {
                            currentGame.Status = Entities.Enum.GameStatus.Turn;
                        }
                        else
                        {
                            currentGame.Status = Entities.Enum.GameStatus.Awaiting;
                        }
                    }
                }
                else if (curUserState == 0)
                {
                    throw new ArgumentException();
                }

                return currentGame;
            }

            throw new AuthenticationException();
        }

        public bool MakeMove(Move move)
        {
            if (move == null)
            {
                throw new ArgumentNullException();
            }
            else if (!Validation.MoveValidation(move))
            {
                throw new ArgumentException();
            }

            World currentGame = this.GetCurrentWorld(move.Token, move.IP, move.GameId);

            if (currentGame == null)
            {
                throw new ArgumentException();
            }

            if (Mechanic.MoveCoordsAllowed(currentGame, move))
            {

            }

            return false;
        }
    }
}
