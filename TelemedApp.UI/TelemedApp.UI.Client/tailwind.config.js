/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./**/*.razor",
        "./Pages/**/*.razor",
        "./Shared/**/*.razor",
        "./Components/**/*.razor",
        "./wwwroot/**/*.html",
        "./wwwroot/**/*.js",
        "./wwwroot/css/**/*.css"
    ],
    theme: {
        extend: {
            screens: {
                xs: "380px",
                ultra: "1600px"
            },
            keyframes: {
                'scale-fade': {
                    '0%': { opacity: '0', transform: 'scale(0.95)' },
                    '100%': { opacity: '1', transform: 'scale(1)' }
                },
                'slide-right': {
                    '0%': { transform: 'translateX(100%)' },
                    '100%': { transform: 'translateX(0)' }
                },
                'slide-left': {
                    '0%': { transform: 'translateX(-100%)' },
                    '100%': { transform: 'translateX(0)' }
                },
                'slide-up': {
                    '0%': { transform: 'translateY(100%)' },
                    '100%': { transform: 'translateY(0)' }
                },
                'toast-enter': {
                    '0%': { opacity: '0', transform: 'translateY(10px) scale(0.95)' },
                    '100%': { opacity: '1', transform: 'translateY(0) scale(1)' }
                }
            },
            animation: {
                'scale-fade': "scale-fade 0.12s ease-out",
                'slide-right': "slide-right 0.25s ease-out",
                'slide-left': "slide-left 0.25s ease-out",
                'slide-up': "slide-up 0.25s ease-out",
                'toast-enter': "toast-enter 0.25s ease-out",
                'fadeIn': "fadeIn 0.3s ease-out"
            },
            transitionProperty: {
                'colors': "background-color, border-color, color, fill, stroke"
            }
        }
    },
    darkMode: "class",
    plugins: []
}