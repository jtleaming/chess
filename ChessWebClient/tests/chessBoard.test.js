var path = require('path');
var fs = require('fs');
var chessBoard = require('../src/chessBoard');

const html = fs.readFileSync(path.join(__dirname, './chessBoard.html'), 'utf-8');
const chessBoardJson = fs.readFileSync(path.join(__dirname, './chessBoard.json'), 'utf-8');

test('convert chess string to chess board and pieces', function () {
    var chessGame = new chessBoard(chessBoardJson);
    expect(chessGame.board.outerHTML).toBe(html);
});