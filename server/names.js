const fs = require('fs');
const Random = require('random-js');

class NameGenerator {

    constructor({filenames=null, names=null}) {
        /** @type {Array<string>} */
        this.allNames = [];
        /** @type {Array<string>} */
        this.freeNames = [];
        /** @type {Set<string>} */
        this.currentNames = new Set();

        this.random = new Random(Random.engines.mt19937().autoSeed());

        if (filenames) {
            this.readNames(filenames);
        }
        if (names) {
            this.addNames(names);
        }
    }

    /**
     * @param {Array<string>} names 
     */
    readNames(filenames) {
        for (filename of filenames) {
            data = fs.readFileSync(filename, 'utf8');
            this.addNames(
                data.split('\n')
                .map(n => n.trim())
                .filter(n => n)
            );
        }
    }

    /**
     * @param {Array<string>} names 
     */
    addNames(names) {
        for (name of names) {
            // Really a set would be better for this runtime-wise, but also it's not that important.
            if (this.allNames.includes(name)) {
                continue;
            }
            this.allNames.push(name);
            this.freeNames.push(name);
        }
    }

    /**
     * @returns {string} A name!
     */
    pickName() {
        if (this.freeNames.length > 0) {
            const randomIndex = this.random.integer(0, this.freeNames.length - 1);
            const name = this.freeNames.splice(randomIndex, 1)[0];

            this.currentNames.add(name);
            return name;
        }
        // Lazy fall back -- add random digits to a name
        for (let i = 0; i < 20; i++) {
            const baseName = this.random.pick(this.allNames);
            const suffix = this.random.string(4, "0123456789");
            const name = baseName + suffix;
            if (this.currentNames.has(name)) {
                continue;
            }

            this.currentNames.add(name);
            return name;
        }
    }

    /**
     * @param {string} name 
     */
    freeUpName(name) {
        
    }

}

module.exports = NameGenerator;