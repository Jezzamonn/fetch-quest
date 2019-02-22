class CallbackState {
    /**
     * @param {function()} callback 
     * @param {number} intervalMs 
     * @param {number} lastTriggerTimeMs 
     */
    constructor(callback, intervalMs, lastTriggerTimeMs) {
        this.callback = callback;
        this.intervalMs = intervalMs;
        this.lastTriggerTimeMs = lastTriggerTimeMs;
    }
}

class FakeIntervalProvider {

    constructor() {
        /** @private {number} Internal count for how much time has elapsed, in ms */
        this.countMs = 0;

        /** @private {?CallbackState} */
        this.callback;
    }

    setInterval(callback, ms) {
        if (this.callback) {
            throw new Error('Can only have 1 callback, thx');
        }
        this.callback = new CallbackState(callback, ms, this.countMs);
    }

    advanceTime(ms) {
        if (!this.callback) {
            this.countMs += ms;
            return;
        }

        while (ms >= this.callback.intervalMs) {
            this.countMs += this.callback.intervalMs;
            ms -= this.callback.intervalMs;

            this.callback.callback();
            this.callback.lastTriggerTimeMs = this.countMs;
        }
        this.countMs += ms;
    }

}

module.exports.FakeIntervalProvider = FakeIntervalProvider;