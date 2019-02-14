using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Xunit;

namespace ConsoleTests
{

    public class ConsoleIntegrationTests
    {
        private StringBuilder output;
        private StreamWriter st;

        public ConsoleIntegrationTests()
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "dotnet";
            proc.StartInfo.UseShellExecute = false;

            proc.StartInfo.RedirectStandardOutput = true;
            output = new StringBuilder();
            proc.StartInfo.Arguments = $"{Directory.GetCurrentDirectory().Replace("IntegrationTests/ConsoleTests", "ChessConsole")}/ChessConsole.dll";
            proc.OutputDataReceived += EventListener;

            proc.StartInfo.RedirectStandardInput = true;
            proc.Start();

            st = proc.StandardInput;
            proc.BeginOutputReadLine();
        }

        private void EventListener(object e, DataReceivedEventArgs args)
        {
            output.Append($"{args.Data}");
        }

        private void Move(string pieaceToMove, string spaceToMove)
        {
            st.WriteLine($"{pieaceToMove} {spaceToMove}");
            Thread.Sleep(100);
        }

        [Fact]
        public void Pawn_EnPassant_CapturesPawn()
        {
            Move("d2", "d4");
            Move("a7", "a6");
            Move("d4", "d5");
            Move("e7", "e5");
            Move("d5", "e6");

            Assert.True(output.ToString().Contains("Pawn captured Pawn e6"));
        }
    }
}