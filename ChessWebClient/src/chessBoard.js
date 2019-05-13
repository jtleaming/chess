class chessBoard {
    constructor(boardString){
        this.board = document.createElement('div');
        this.board.className = 'chessboard';
        this.chessPiecesBlack = {
            'rook': '&#9820;',
            'knight': '&#9822;',
            'bishop': '&#9821;',
            'king': '&#9819;',
            'queen': '&#9818',
            'pawn': '&#9823;',
            '': ''
        };
        this.chessPiecesWhite = {
            'rook': '&#9814;',
            'knight': '&#9816;',
            'bishop': '&#9815;',
            'king': '&#9813;',
            'queen': '&#9812;',
            'pawn': '&#9817;',
            '': ''
        }

        this.createBoard(boardString);
    }

    createBoard(boardString) {
        let squares = Object.entries(JSON.parse(boardString));
        var first = 'black';
        var second = 'white';
        for (let index = 57; index >= 1; index++) {
            let square = squares[index-1];
            let squareId = square[0];
            let div = document.createElement('div');
            div.id = squareId;
            if(squareId[1] % 2 === 1) {
                first = 'black';
                second = 'white';
            } else {
                first = 'white';
                second = 'black';
            }

            if(squareId[0].charCodeAt(0) % 2 === 1) {
                div.className = first;
            } else {
                div.className = second;
            }

            if(square[1].Occupied){
                if(squareId[1] === '1' || squareId[1] === '2') {
                    div.innerHTML = this.chessPiecesWhite[square[1].Piece.Type.toLowerCase()];
                } else {
                    div.innerHTML = this.chessPiecesBlack[square[1].Piece.Type.toLowerCase()];
                }
            }

            this.board.appendChild(div);

            if (index % 8 === 0) {
                index -= 16;
            }
        }
        var body = document.getElementsByTagName('body')[0];
        body.appendChild(this.board);
    }
}

module.exports = chessBoard;