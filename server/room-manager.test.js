const { RoomManager } = require("./room-manager");
const { NameGenerator } = require("./names");

jest.useFakeTimers();

test('picks room', () => {
    const nameGenerator = new NameGenerator({filenames: ['top100names.txt']});
    const roomManager = new RoomManager(nameGenerator, 1);

    const room = roomManager.getRoom('1');

    expect(room).toBeTruthy();
});

test(`doesn't recycle names after a short time period`, () => {
    const nameGenerator = new NameGenerator({filenames: ['top100names.txt']});
    const roomManager = new RoomManager(nameGenerator, 1);

    roomManager.getRoom('1');

    jest.advanceTimersByTime(500);

    expect(roomManager.names).toHaveProperty('1');
});

test('recycles unused names', () => {
    const nameGenerator = new NameGenerator({filenames: ['top100names.txt']});
    const roomManager = new RoomManager(nameGenerator, 1);

    roomManager.getRoom('1');

    jest.advanceTimersByTime(1500);

    expect(roomManager.names).not.toHaveProperty('1');
});

test(`doesn't recycle used names`, () => {
    const nameGenerator = new NameGenerator({filenames: ['top100names.txt']});
    const roomManager = new RoomManager(nameGenerator);
 
    roomManager.getRoom('1');

    jest.advanceTimersByTime(500);

    roomManager.getRoom('1');

    jest.advanceTimersByTime(1000);

    expect(roomManager.names).toHaveProperty('1');
});