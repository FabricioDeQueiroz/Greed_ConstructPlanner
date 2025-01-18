/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "../**/*.{html,js,razor}",
    "!../node_modules/**"
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('daisyui'),
  ],
  daisyui: {
    themes: ["dark"],
  },
}