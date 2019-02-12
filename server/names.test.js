const NameGenerator = require("./names");

test('ignores duplicate names', () => {
    const generator = new NameGenerator(['Lucky', 'Lucky', 'Maxwell']);
   
    expect(generator.allNames).toHaveLength(2);
});

test('generates a names', () => {
    const generator = new NameGenerator(['Lucky', 'Lucky', 'Maxwell']);

    const name = generator.pickName();

    expect(name).toBeTruthy();
});

test('generates heaps of unique names', () => {
    const generator = new NameGenerator(['Lucky', 'Lucky', 'Maxwell']);

    const names = new Set();
    for (let i = 0; i < 100; i++) {
        names.add(generator.pickName());
    }

    expect(names).toHaveLength(100);
});