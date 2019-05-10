var path = require('path');
var html = path.join(__dirname, './chessBoard.html');

test('convert chess string to chess board and pieces', function () {
    var chessBoardString = `R  N  B  Q  K  B  N  R
a7 P  P  P  e7 P  P  P
P  b6 c6 d6 P  f6 g6 h6
a5 b5 c5 d5 e5 f5 g5 h5
a4 b4 c4 d4 e4 f4 g4 h4
a3 b3 c3 d3 e3 f3 g3 h3
P  P  P  d2 P  P  P  P
R  N  B  Q  K  B  N  R`;
    var chessDiv = document.createElement(html);
    var chessBoard = new chessBoard(chessBoardString);
    expect(chessBoard.board).toBe(chessDiv);
});