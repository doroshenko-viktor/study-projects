addEventListener('message', (event) => {
    if (isWorkOver(event.data)) {
        postMessage('Bie, until tomorrow!');
        return close();
    }

    var response = getResponse(event.data);
    postMessage(response);
});

function isWorkOver(message) {
    return message === 'Work day is over';
}

function getResponse(message) {
    if (message === 'Hi, worker!') {
        return 'Hi, main!';
    } else {
        return 'I\'m working...';
    }
}