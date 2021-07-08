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

const useRooms = false;

const debugUrl = 'localhost:3000';
const regularUrl = 'http://35.231.246.171:3000';
const useDebugUrl = false;

const url = useDebugUrl ? debugUrl : regularUrl;

function init() {
	lastTime = Date.now();
	controller = new Controller();

	console.log(`useRooms = ${useRooms}`);
	const enterRoomContent = document.querySelector('#enter-room-content');
	const gameContent = document.querySelector('#game-content');
	if (useRooms) {
		enterRoomContent.classList.remove('hidden');
		gameContent.classList.add('hidden');
	}
	else {
		enterRoomContent.classList.add('hidden');
		gameContent.classList.remove('hidden');
	}

	handleResize();
	// Set up event listeners.
	window.addEventListener('resize', handleResize);
	// Kick off the update loop
	window.requestAnimationFrame(everyFrame);

	document.querySelector('.button-bad').addEventListener('click', () => sendMessage('bad'))
	document.querySelector('.button-good').addEventListener('click', () => sendMessage('good'))

	// Connect to socket.io!
	socket = io(url);

	socket.on('goal-update', message => updateGoal(message));

	beRandomColor();
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

function beRandomColor() {
	const body = document.querySelector('body');
	const color = hsl(Math.random(), 0.6, 0.2);
	body.style.backgroundColor = color;
}

function sendMessage(message) {
	socket.emit('reaction', message);
}

function updateGoal(message) {
	const goal = document.querySelector('#goal-message');
	// Should be safe from XSS because it's textContent
	goal.textContent = message;
}

window.onload = init;