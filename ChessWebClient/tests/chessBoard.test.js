var path = require('path');
var html = path.join(__dirname, './chessBoard.html');
var chessBoardJson = path.join(__dirname, './chessBoard.json');

test('convert chess string to chess board and pieces', function () {
    var chessBoardString = chessBoardJson;
    console.log(chessBoardJson);
    var chessDiv = document.createElement(html);
    var chessBoard = new chessBoard(chessBoardString);
    expect(chessBoard.board).toBe(chessDiv);
});