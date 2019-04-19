const { NameGenerator } = require("./names");

class RoomManager {

    /**
     * @param {!NameGenerator} nameGenerator
     * @param clearNameTime How long to a name needs to be unused before we clear it, in seconds.
     */
    constructor(nameGenerator, clearNameTime) {
        this.nameGenerator = nameGenerator;
        // Map of names to ids.
        this.names = {};

        this.usedNames = new Set();

        setInterval(() => this.clearUnusedNames(), clearNameTime * 1000)
    }

    getRoom(id) {
        if (id in this.names) {
            const name = this.names[id];
            this.usedNames.add(name);
            return name;
        }
        const newName = this.nameGenerator.pickName();
        this.names[id] = newName;
        this.usedNames.add(name);
        return name;
    }

    clearUnusedNames() {
        // Interestingly, it's totally fine to delete properties while looping over them
        for (name in this.names) {
            if (!this.usedNames.has(name)) {
                delete this.names[name];
            }
        }
    }

}

module.exports.RoomManager = RoomManager;