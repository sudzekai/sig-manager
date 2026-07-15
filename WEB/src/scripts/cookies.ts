export function setCookie<T>(name: string, value: T) {
    document.cookie = `${name}=${encodeURIComponent(JSON.stringify(value))}; path=/; max-age=31536000; SameSite=Lax`;
}

export function getCookie<T>(name: string): T | null {
    const cookies = document.cookie.split("; ");

    const cookie = cookies.find(x => x.startsWith(`${name}=`));

    if (!cookie) return null;

    try {
        return JSON.parse(decodeURIComponent(cookie.split("=")[1])) as T;
    }
    catch {
        return null;
    }
}