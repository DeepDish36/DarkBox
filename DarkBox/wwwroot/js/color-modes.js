// wwwroot/js/color-modes.js
document.addEventListener('DOMContentLoaded', function () {
    const theme = localStorage.getItem('theme') || 'auto';
    setTheme(theme);

    document.querySelectorAll('[data-bs-theme-value]').forEach(toggle => {
        toggle.addEventListener('click', () => {
            const theme = toggle.getAttribute('data-bs-theme-value');
            setTheme(theme);
        });
    });

    function setTheme(theme) {
        document.documentElement.setAttribute('data-bs-theme', theme);
        localStorage.setItem('theme', theme);
    }
});
