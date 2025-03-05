/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './**/*.{razor,html}',
    './**/(Layout|Pages)/*.{razor,html}', // Include only Layout and Pages folders
  ],
  theme: {
    colors:{
      banana: "#ffcc00",
    },
    extend: {},
  },
  plugins: [],
}