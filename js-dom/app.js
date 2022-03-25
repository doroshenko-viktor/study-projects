const $titleById = document.getElementById("main-title");
const $listByQuery = document.querySelector("ul");
const $listElements = $listByQuery.getElementsByTagName("li");
const $firstListElem = document.querySelector("ul li:first-child");
const $input = document.getElementById("text-input");

$titleById.classList.add('red');
$titleById.style.fontStyle = "italic";

console.dir($titleById);
console.dir($listByQuery);
console.dir($listElements);
console.log($firstListElem);

const inputValue = $input.getAttribute("value");
console.log(inputValue);

$input.setAttribute("value", "new text");

console.log($listByQuery.innerText);
console.log($listByQuery.innerHTML);