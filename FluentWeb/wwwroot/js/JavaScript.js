window.moveElementToBody = (element) => {
    if (element && element.parentElement) {
        document.body.appendChild(element);
    }
};
// dragDropHelper.js
window.preventDefault = function (event) {
    if (event && typeof event.preventDefault === "function") {
        event.preventDefault();
    } else {
        console.error("Invalid event passed to preventDefault");
    }
};
// dragDropHelper.js
window.handleDragOver = function (event) {
    // Previene el comportamiento predeterminado
    window.preventDefault(event);
};