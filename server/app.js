const io = require('socket.io')(3000);
const { RoomManager } = require('./room-manager');
const { NameGenerator } = require("./names");

const nameGenerator = new NameGenerator({filenames: ['top100names.txt', 'submittednames.txt']})
const roomManager = new RoomManager(nameGenerator, 30 * 60);

io.on('connection', (socket) => {
    console.log('A user totes connected, hey');

    let room = null;

    socket.on('request-room', (message) => {
        const guid = message;
        room = roomManager.getRoom(guid);
        socket.join(roomName);
    });

    socket.on('reaction', (message) => {
        if (room === null) {
            console.log(`Got reaction "${message}", but no room`);
            return;
        }

        console.log(`Got reaction "${message}"`);
        // TODO: Only send to the main dude
        socket.to(room).emit('reaction', message);
    });

    socket.on('goal-update', (message) => {
        if (room === null) {
            console.log(`Got goal ${message}, but no room`);
        }
        console.log(`New goal: ${message}`);
        socket.broadcast.emit('goal-update', message);
    });
    
});