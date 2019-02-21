const { NameGenerator } = require("./names");

let defaultGenerator;

beforeEach(() => {
    defaultGenerator = new NameGenerator({names: ['Lucky', 'Maxwell', 'Daisy']});
});

test('ignores duplicate names', () => {
    const generator = new NameGenerator({names: ['Lucky', 'Lucky', 'Maxwell']});
   
    expect(generator.allNames).toHaveLength(2);
});

test('generates a name', () => {
    const generator = defaultGenerator;

    const name = generator.pickName();

    expect(name).toBeTruthy();
});

test('generates lots of unique names', () => {
    const generator = defaultGenerator;

    const names = new Set();
    for (let i = 0; i < 100; i++) {
        names.add(generator.pickName());
    }

    expect(Array.from(names)).toHaveLength(100);
});

test('generates heaps of unique names', () => {
    const generator = defaultGenerator;

    const names = new Set();
    for (let i = 0; i < 10000; i++) {
        names.add(generator.pickName());
    }

    expect(Array.from(names)).toHaveLength(10000);
});

test('exhausts submitted names first', () => {
    const generator = defaultGenerator;

    const names = [];
    for (let i = 0; i < 3; i++) {
        names.push(generator.pickName());
    }

    expect(names.sort()).toEqual(['Lucky', 'Maxwell', 'Daisy'].sort());
});

test('reuses returned names', () => {
    const generator = new NameGenerator({names: ['Lucky']});

    const name = generator.pickName();
    expect(name).toEqual('Lucky');

    generator.freeUpName(name);

    const secondName = generator.pickName();
    expect(secondName).toEqual('Lucky');
});

test('freeing a name doesn\'t free other names', () => {
    const generator = new NameGenerator({names: ['Lucky']});

    generator.pickName();
    const name = generator.pickName();
    generator.freeUpName(name);

    const secondName = generator.pickName();
    expect(secondName).not.toEqual('Lucky');
});

test('throws error if there are no names given', () => {
    expect(() => new NameGenerator()).toThrow();
});

test('loads from file', () => {
    const generator = new NameGenerator({filenames: ['submittednames.txt']});

    const name = generator.pickName();

    expect(name).toBeTruthy();
});

test('loads from multiple files', () => {
    const generator = new NameGenerator({filenames: ['submittednames.txt', 'top100names.txt']});

    const name = generator.pickName();

    expect(name).toBeTruthy();
})