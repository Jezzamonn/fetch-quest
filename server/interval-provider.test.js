const { IntervalProvider } = require('./interval-provider');

test('interval triggers after 1 second', done => {
    const provider = new IntervalProvider();

    provider.setInterval(() => done(), 1000);
});

test('throws error if you try to add multiple callbacks', () => {
    const provider = new IntervalProvider();
    provider.setInterval(() => {}, 1000);

    expect(() => provider.setInterval(() => {}, 1000)).toThrow();
})