const { FakeIntervalProvider } = require('./fake-interval-provider');

test('interval triggers after 1 second', () => {
    const provider = new FakeIntervalProvider();
    const callback = jest.fn();

    provider.setInterval(callback, 1000);
    provider.advanceTime(1000);

    expect(callback).toBeCalled();
});

test('interval doesn\'t trigger when not enough time has passed', () => {
    const provider = new FakeIntervalProvider();
    const callback = jest.fn();

    provider.setInterval(callback, 1000);
    provider.advanceTime(999);

    expect(callback).not.toBeCalled();
});

test('interval gets triggered multiple times', () => {
    const provider = new FakeIntervalProvider();
    const callback = jest.fn();

    provider.setInterval(callback, 1000);
    provider.advanceTime(3000);

    expect(callback).toBeCalledTimes(3);
});

test('throws error if you try to add multiple callbacks', () => {
    const provider = new FakeIntervalProvider();
    provider.setInterval(() => {}, 1000);

    expect(() => provider.setInterval(() => {}, 1000)).toThrow();
})