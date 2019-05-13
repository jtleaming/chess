class chessBoard {
    constructor(boardString){
        this.board = document.createElement('div');
        this.board.className = 'chessboard';
        this.chessPieces = {
            'rook': '&#9820;',
            'knight': '&#9822;',
            'bishop': '&#9821;',
            'king': '&#9819;',
            'queen': '&#9818',
            'pawn': '&#9821;',
            '': ''
        }

        this.createBoard(boardString);
    }

    createBoard(boardString) {
        let squares = JSON.parse(boardString);
        let rows = ['a','b','c','d','e','f','g','h'];
        for (let index = 0; index < 8; index++) {
            let row = document.createElement('div');
            row.id = `row-${index}`;
            // rows.push(row);
            for (let a = 0; a < rows.length; a++) {
                let squareId = `${rows[a]}${a+1}`
                let square = document.createElement('div');
            }
        }
        var body = document.getElementsByTagName('body')[0];
        body.appendChild(this.board);
    }
}

module.exports = chessBoard;