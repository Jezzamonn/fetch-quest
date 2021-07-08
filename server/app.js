const io = require('socket.io')(3000, {
    cors: {
        origin: '*',
    }
});
const { RoomManager } = require('./room-manager');
const { NameGenerator } = require("./names");

const nameGenerator = new NameGenerator({filenames: ['top100names.txt', 'submittednames.txt']})
const roomManager = new RoomManager(nameGenerator, 30 * 60);

const useRooms = false;

io.on('connection', (socket) => {
    console.log(`User connected. ID = ${socket.id}`);

    let room = null;

    if (useRooms) {
        socket.on('request-room', (message) => {
            const guid = message;
            room = roomManager.getRoom(guid);
            socket.join(roomName);
        });
    }

    socket.on('reaction', (message) => {
        if (useRooms && room === null) {
            console.log(`Got reaction "${message}", but no room`);
            return;
        }

        console.log(`Got reaction "${message}"`);
        // TODO: Only send to the main dude
        if (useRooms) {
            socket.to(room).emit('reaction', message);
        }
        else {
            socket.broadcast.emit('reaction', message);
        }
    });

    socket.on('goal-update', (message) => {
        if (useRooms && room === null) {
            console.log(`Got goal ${message}, but no room`);
        }
        console.log(`New goal: ${message}`);
        socket.broadcast.emit('goal-update', message);
    });

});