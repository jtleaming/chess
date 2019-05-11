using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using FluentAssertions;
using Xunit;

namespace ConsoleTests
{

    public class ConsoleIntegrationTests : IDisposable
    {
        private StringBuilder output;
        private StreamWriter st;
        //Set to false to watch tests in debug console
        private bool redirectOutput = false;
        private Process proc;

        public ConsoleIntegrationTests()
        {
            proc = new Process();
            proc.StartInfo.FileName = "dotnet";
            proc.StartInfo.UseShellExecute = false;

            proc.StartInfo.RedirectStandardOutput = redirectOutput;
            proc.StartInfo.RedirectStandardError = true;
            output = new StringBuilder();
            proc.StartInfo.Arguments = $"{Directory.GetCurrentDirectory().Replace("IntegrationTests\\ConsoleTests", "ChessConsole")}\\ChessConsole.dll";
            proc.OutputDataReceived += EventListener;

            proc.StartInfo.RedirectStandardInput = true;
            proc.Start();

            st = proc.StandardInput;
            if(redirectOutput) proc.BeginOutputReadLine();
        }

        private void EventListener(object e, DataReceivedEventArgs args)
        {
            output.Append($"{args.Data}");
        }

        private void Move(string pieaceToMove, string spaceToMove)
        {
            st.WriteLineAsync($"{pieaceToMove} {spaceToMove}");
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

            output.ToString().Should().Contain( "Pawn captured Pawn e6");
        }

        [Fact]
        public void CastlingNotationTest()
        {
            Move("g1", "f3"); Move("g8", "f6");
            Move("c2", "c4"); Move("g7", "g6");
            Move("b1", "c3"); Move("f8", "g7");
            Move("d2", "d4"); Move("O", "O");
        }

        [Fact]
        public void TestForCheck()
        {
            //A bit of fun chess trivia. This game play is taken from The Game of The Century played in 1956, Donaly Byrne vs Bobby Fischer.

            Move("g1", "f3"); Move("g8", "f6");
            Move("c2", "c4"); Move("g7", "g6");
            Move("b1", "c3"); Move("f8", "g7");
            Move("d2", "d4"); Move("e8", "g8");
            Move("c1", "f4"); Move("d7", "d5");
            Move("d1", "b3"); Move("d5", "c4");
            Move("b3", "c4"); Move("c7", "c6");
            Move("e2", "e4"); Move("b8", "d7");
            Move("a1", "d1"); Move("d7", "b6");
            Move("c4", "c5"); Move("c8", "g4");
            Move("f4", "g5"); Move("b6", "a4");
            Move("c5", "a3"); Move("a4", "c3");
            Move("b2", "c3"); Move("f6", "e4");
            Move("g5", "e7"); Move("d8", "b6");
            Move("f1", "c4"); Move("e4", "c3");
            Move("e7", "c5"); Move("f8", "e8");
            Move("e1", "f1"); Move("g4", "e6");
            Move("c5", "b6"); Move("e6", "c4"); //This move puts player one in check
            Move("f1", "g1");//This is the move to take player one out of check
            Move("c3", "e2");
            Move("g1", "f1"); Move("e2", "d4");
            Move("f1", "g1"); Move("d4", "e2");
            Move("g1", "f1"); Move("e2", "c3");
            Move("f1", "g1"); Move("a7", "b6");
            Move("a3", "b4"); Move("a8", "a4");
            Move("b4", "b6"); Move("c3", "d1");
            Move("h2", "h3"); Move("a4", "a2");
            Move("g1", "h2"); Move("d1", "f2");
            Move("h1", "e1"); Move("e8", "e1");
            Move("b6", "d8"); Move("g7", "f8");
            Move("f3", "e1"); Move("c4", "d5");
            Move("e1", "f3"); Move("f2", "e4");
            Move("d8", "b8"); Move("b7", "b5");
            Move("h3", "h4"); Move("h7", "h5");
            Move("f3", "e5"); Move("g8", "g7");
            Move("h2", "g1"); Move("f8", "c5");
            Move("g1", "f1"); Move("e4", "g3");
            Move("f1", "e1"); Move("c5", "b4");
            Move("e1", "d1"); Move("d5", "b3");
            Move("d1", "c1"); Move("g3", "e2");
            Move("c1", "b1"); Move("e2", "c3");
            Move("b1", "c1"); Move("a2", "c2");
        }

        public void Dispose()
        {
            proc.Kill();
            proc.Dispose();
        }
    }
}