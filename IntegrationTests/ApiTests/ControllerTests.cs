using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace ApiTests
{
    public class ControllerTests : IDisposable
    {
        private Process proc;
        private readonly HttpClient client = new HttpClient();
        private static string currentDirectory = Directory.GetCurrentDirectory();
        private readonly string chessBoardJson = File.ReadAllText($"{currentDirectory}\\Fixtures\\chessboard.json");
        private readonly string playersJson = File.ReadAllText($"{currentDirectory}\\Fixtures\\players.json");
        public ControllerTests()
        {
            proc = new Process();
            proc.StartInfo.FileName = "dotnet";
            proc.StartInfo.Arguments = $"{currentDirectory.Replace("IntegrationTests\\ApiTests", "ChessApi")}\\ChessApi.dll";
            proc.Start();
        }


        [Fact]
        public async Task StartGameController_ReturnsChessBoardJsonAsync()
        {
            var response = await client.GetAsync("http://localhost:5000/startgame");
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(chessBoardJson, responseContent);
        }
        [Fact]
        public async Task GetPlayerController_ReturnsPlayersJsonAsync()
        {
            await client.GetAsync("http://localhost:5000/startgame");
            var response = await client.GetAsync("http://localhost:5000/players");
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(playersJson, responseContent);
        }
        public void Dispose()
        {
            proc.Kill();
            proc.Dispose();
        }
    }
}
