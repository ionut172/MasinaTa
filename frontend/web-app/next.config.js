/** @type {import('next').NextConfig} */
const nextConfig = {
    experimental: {
        serverActions:true
    },
    reactStrictMode: true,
    images: {
        domains: [
            "cdn.pixabay.com",
            "pixabay.com"
        ]
    }
}

module.exports = nextConfig
