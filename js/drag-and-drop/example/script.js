const $item = document.getElementById('item');
const $boxes = document.getElementsByClassName('box');
console.dir($boxes);

$item.addEventListener('dragstart', (event) => {
    console.dir(event);
    event.dataTransfer.setData('text/plain', event.target.id);
    setTimeout(() => event.target.classList.add('hide'), 0);
});

$item.addEventListener('dragend', (event) => {
    console.dir(event);
    event.target.classList.remove('hide');
})

for (const box of $boxes) {
    box.addEventListener('dragenter', (event) => {
        event.preventDefault();
        event.target.classList.add('drag-over');
    });

    box.addEventListener('dragover', (event) => {
        event.preventDefault();
        event.target.classList.add('drag-over');
    });

    box.addEventListener('dragleave', (event) => {
        event.target.classList.remove('drag-over');
    });

    box.addEventListener('drop', (event) => {
        event.target.classList.remove('drag-over');
        const id = event.dataTransfer.getData('text/plain');
        const item = document.getElementById(id);
        event.target.appendChild(item);
        item.classList.remove('hide');
    });
}