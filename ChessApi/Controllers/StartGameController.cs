using System.Collections.Generic;
using ChessEngine;
using ChessEngine.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

                return Ok(JsonConvert.SerializeObject(chessGame.Board.Squares));
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
                return Ok(JsonConvert.SerializeObject(chessGame.Players));
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

    }
}
