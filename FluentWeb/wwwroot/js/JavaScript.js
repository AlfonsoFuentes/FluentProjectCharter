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