import io from 'socket.io-client';

import Controller from './controller.js';
import { hsl } from './util.js';

let canvas = document.getElementById('canvas');
let context = canvas.getContext('2d');

// Currently assuming square proportions.
const SIZE = 500;

let scale = 1;
let lastTime;
let controller;

let socket;

function init() {
	lastTime = Date.now();
	controller = new Controller();

	handleResize();
	// Set up event listeners.
	window.addEventListener('resize', handleResize);
	// Kick off the update loop
	window.requestAnimationFrame(everyFrame);

	document.addEventListener('mousedown', () => sendMessage());
	document.addEventListener('touchstart', () => sendMessage());

	// Connect to socket.io!
	socket = io('http://localhost:3000');
}

// TODO: Make tweak this to allow frame skipping for slow computers. Maybe.
function everyFrame() {
	update();
	render();
	requestAnimationFrame(everyFrame);
}

function update() {
	let curTime = Date.now();
	let dt = (curTime - lastTime) / 1000;
	controller.update(dt);
	lastTime = curTime;
}

function render() {
	// Clear the previous frame
	context.resetTransform();
	context.clearRect(0, 0, canvas.width, canvas.height);

	// Set origin to middle and scale canvas
	context.scale(scale, scale);

	controller.render(context);
}

function handleResize(evt) {
	let pixelRatio = window.devicePixelRatio || 1;
	let width = window.innerWidth;
	let height = window.innerHeight;

	canvas.width = width * pixelRatio;
	canvas.height = height * pixelRatio;
	canvas.style.width = width + 'px';
	canvas.style.height = height + 'px';

	// Math.max -> no borders (will cut off edges of the thing)
	// Math.min -> show all (with borders)
	// There are other options too :)
	scale = Math.max(canvas.width, canvas.height) / SIZE;

	render();
}

function sendMessage() {
	const body = document.querySelector('body');
	const color = hsl(Math.random(), 0.6, 0.2);
	body.style.backgroundColor = color;

	socket.emit('reaction', 'good');
}

init();