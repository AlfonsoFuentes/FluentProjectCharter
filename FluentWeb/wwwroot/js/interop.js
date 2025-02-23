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
window.syncScroll = () => {
    const timeline = document.querySelector('.gantt-timeline');
    const header = document.querySelector('.gantt-header');

    if (!timeline || !header) {
        console.warn("Elementos .gantt-timeline o .gantt-header no encontrados.");
        return;
    }

    timeline.addEventListener('scroll', () => {
        header.scrollLeft = timeline.scrollLeft;
    });
};
window.syncRowHeights = () => {
    const sidebarItems = document.querySelectorAll('.gantt-sidebar-item');
    const rows = document.querySelectorAll('.gantt-row');
    if (!sidebarItems || !rows) {
        console.warn("Elementos .gantt-timeline o .gantt-header no encontrados.");
        return;
    }
    // Itera sobre los elementos y ajusta el alto máximo
    for (let i = 0; i < Math.max(sidebarItems.length, rows.length); i++) {
        const sidebarItem = sidebarItems[i];
        const row = rows[i];

        if (sidebarItem && row) {
            const maxHeight = Math.max(
                sidebarItem.offsetHeight,
                row.offsetHeight
            );

            sidebarItem.style.height = `${maxHeight}px`;
            row.style.height = `${maxHeight}px`;
        }
    }
};