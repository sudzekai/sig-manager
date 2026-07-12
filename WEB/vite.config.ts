import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'

export default defineConfig({
    plugins: [
        react(),
        tailwindcss()],

    server: {
        proxy: {
            '/api': {
                target: 'http://webshvets.ru:8080/api',
                changeOrigin: true,
                rewrite: (path) => path.replace(/^\/api/, ''),
            }
        },
        host: '0.0.0.0',
        port: 80,
        allowedHosts: [
            'webshvets.ru'
        ]
    }
})