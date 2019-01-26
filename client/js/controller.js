export default class Controller {

	constructor() {
		this.animAmt = 0;
		this.period = 1;
	}

	update(dt) {
		this.animAmt += dt / this.period;
		this.animAmt %= 1;
	}

	render(context) {
		context.beginPath();
		context.strokeStyle = 'white';
		context.lineWidth = 2;
		context.globalAlpha = 0.1;

		const lines = 20;
		for (let i = 0; i < lines + 1; i ++) {
			const mult = 1 / (lines - 1);
			context.beginPath();
			context.moveTo(0, 500 * (i - this.animAmt) * mult);
			context.lineTo(500, 500 * (i + 1 - this.animAmt) * mult);
			context.stroke();
		}
	}

}
