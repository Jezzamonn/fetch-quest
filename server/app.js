const io = require('socket.io')(3000);

io.on('connection', (socket) => {
    console.log('A user totes connected, hey');

    socket.on('reaction', (message) => {
        console.log(`Got reaction "${message}"`);
        // TODO: Only send to the main dude
        socket.broadcast.emit('reaction', message);
    });

    socket.on('goal-update', (message) => {
        console.log(`New goal: ${message}`);
        socket.broadcast.emit('goal-update', message);
    });
    
});