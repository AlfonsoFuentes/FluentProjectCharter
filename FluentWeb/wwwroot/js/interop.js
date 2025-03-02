window.getWindowDimensions = () => {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};
window.onWindowResize = (dotNetHelper) => {
    window.addEventListener('resize', () => {
        const dimensions = {
            width: window.innerWidth,
            height: window.innerHeight
        };
        dotNetHelper.invokeMethodAsync('UpdateWindowDimensions', dimensions);
    });
};
// Función genérica para calcular el ancho de una columna
window.calculateColumnWidth = (columnClass) => {
    const cells = document.querySelectorAll(`.${columnClass}`);
    let maxWidth = 0;

    // Iterar sobre todas las celdas de la columna para encontrar el ancho máximo
    cells.forEach(cell => {
        const computedStyle = window.getComputedStyle(cell);
        const padding = parseFloat(computedStyle.paddingLeft) + parseFloat(computedStyle.paddingRight);
        const border = parseFloat(computedStyle.borderLeftWidth) + parseFloat(computedStyle.borderRightWidth);
        const width = cell.scrollWidth + padding + border;

        if (width > maxWidth) {
            maxWidth = width;
        }
    });

    // Devolver el ancho máximo
    return maxWidth;
};
document.addEventListener("DOMContentLoaded", function () {
    const header = document.querySelector(".table-header");
    const table = document.querySelector(".scrollable-table");

    if (header && table) {
        table.addEventListener("scroll", () => {
            header.scrollLeft = table.scrollLeft;
        });
    }
});