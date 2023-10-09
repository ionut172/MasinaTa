/** @type {import('next').NextConfig} */
const nextConfig = {
    experimental: {
        serverActions:true,
        esmExternals: false
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
