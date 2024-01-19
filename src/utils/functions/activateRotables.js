export default () => {
    const rotatableElements = document.querySelectorAll('.rotable');
    rotatableElements.forEach(el => {
        el.addEventListener('mouseenter', () => {
            el.style.transform="rotate(360deg)"
        });
        el.addEventListener('mouseleave', () => {
            el.style.transform="rotate(0)"
        });
    });
}