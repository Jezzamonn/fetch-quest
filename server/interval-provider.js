class IntervalProvider {

    constructor() {
        this.hasCallback = false;
    }

    setInterval(callback, ms) {
        if (this.hasCallback) {
            throw new Error('Can only have 1 callback, thx');
        }
        this.hasCallback = true;
        setInterval(callback, ms);
    }

}

module.exports.IntervalProvider = IntervalProvider;