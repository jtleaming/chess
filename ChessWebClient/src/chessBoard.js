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
            'pawn': '&#9821;'
        }

        this.createBoard(`R  N  B  Q  K  B  N  R
a7 P  P  P  e7 P  P  P
P  b6 c6 d6 P  f6 g6 h6
a5 b5 c5 d5 e5 f5 g5 h5
a4 b4 c4 d4 e4 f4 g4 h4
a3 b3 c3 d3 e3 f3 g3 h3
P  P  P  d2 P  P  P  P
R  N  B  Q  K  B  N  R`);
    }

    createBoard(boardString) {
        boardString = boardString.replace(/\s/g, '');
        for (let index = 0; index < boardString.length; index++) {
            const element = boardString[index];
            var square = document.createElement('div');
            if(index%2 === 0) {
                square.className = 'white';
            } else {
                square.className = 'black';
            }
            this.board.appendChild(square);
        }
        var body = document.getElementsByTagName('body')[0];
        body.appendChild(this.board);
    }
}