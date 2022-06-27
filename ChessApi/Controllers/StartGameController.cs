using System.Text.Json;
using ChessEngine;
using ChessEngine.Common;
using Microsoft.AspNetCore.Mvc;

namespace ChessApi.Controllers
{
    [ApiController]
    public class StartGameController : ControllerBase
    {
        private static Game chessGame;

        [HttpGet]
        [Route("startgame")]
        public ActionResult<string> StartGame()
        {
            try
            {
                chessGame = new Game();
                var enPassantChecker = new EnPassant();
                chessGame.CreateGame(enPassantChecker);

                return Ok(JsonSerializer.Serialize(chessGame.Board.Squares));
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("players")]
        public ActionResult<string> GetPlayers()
        {
            try
            {
                return Ok(JsonSerializer.Serialize(chessGame.Players));
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

    }
}
