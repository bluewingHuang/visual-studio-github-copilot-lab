// Initialize theme immediately to prevent flash of wrong theme
(function () {
    var theme = localStorage.getItem('theme') || 'light';
    document.documentElement.setAttribute('data-theme', theme);
})();

window.themeToggle = {
    getTheme: function () {
        return localStorage.getItem('theme') || 'light';
    },
    setTheme: function (theme) {
        localStorage.setItem('theme', theme);
        document.documentElement.setAttribute('data-theme', theme);
    }
};
