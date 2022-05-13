const $div = document.getElementById('movable');

let newX = 0, newY = 0, startX = 0, startY = 0;

$div.addEventListener('mousedown', (event) => {
    event.preventDefault();

    startX = event.pageX;
    startY = event.pageY;

    document.addEventListener('mousemove', mouseMove);

    document.addEventListener('mouseup', () => {
        document.removeEventListener('mousemove', mouseMove);
    });
});

function mouseMove(event) {
    newX = startX - event.clientX;
    newY = startY - event.clientY;

    startX = event.clientX;
    startY = event.clientY;

    const newTop = $div.offsetTop - newY;
    const newLeft = $div.offsetLeft - newX;

    if (newTop + $div.offsetHeight < window.innerHeight - 50 && newTop > 50) {
        $div.style.top = newTop + "px";
    }

    if (newLeft + $div.offsetWidth < window.innerWidth - 50 && newLeft > 50) {
        $div.style.left = newLeft + "px";
    }
}