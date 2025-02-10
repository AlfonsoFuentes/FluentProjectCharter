window.moveElementToBody = (element) => {
    if (element && element.parentElement) {
        document.body.appendChild(element);
    }
};