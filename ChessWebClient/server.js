var express = require('express');
var path = require('path');
var open = require('open');

var port = 3131;
var app = express();
var htmlPath = path.join(__dirname, './src/index.html');

app.use(express.static('src'));

app.get('/', function (req, res, next) {
    res.set('content-type', 'text/html');
    res.sendFile(htmlPath);
});

app.listen(port, function (err) {
    if (err) {
        console.log(err);
    } else {
        open('http://localhost:' + port);
    }
});