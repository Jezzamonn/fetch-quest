const fs = require('fs');

class NameGenerator {

    constructor({filenames=null, names=null}) {
        /** @type {Array<string>} */
        this.allNames = [];
        /** @type {Array<string>} */
        this.freeNames = [];
        /** @type {Set<string>} */
        this.currentNames = new Set();

        if (filenames) {
            this.readNames(filenames);
        }
        if (names) {
            this.addNames(filenames);
        }
    }

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

    addNames(names) {
        for (name of names) {
            // Really a set would be better for this runtime-wise, but also it's not that important.
            if (allNames.includes(name)) {
                continue;
            }
            allNames.push(name);
        }
    }

    pickName() {
        
    }

    freeUpName(name) {
        
    }

}

module.exports = NameGenerator;