let worker = new Worker('./worker.js');

worker.addEventListener('message', (event) => {
    console.log(`Worker said: ${event.data}`);
})

console.log('Saying "Hi" to worker');
worker.postMessage('Hi, worker!');

setTimeout(() => {
    console.log('Requesting worker\'s status');
    worker.postMessage('Hey, worker, how\'s it going?');

    setTimeout(() => {
        console.log('Signalling about end of workday');
        worker.postMessage('Work day is over');
    }, 100);
}, 100);

