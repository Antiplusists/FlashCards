﻿export function setCookie(name: string, value: string, expires: number | Date | undefined) {

    let updatedCookie = encodeURIComponent(name) + "=" + encodeURIComponent(value);
    
    if (expires instanceof Date) {
        updatedCookie += `;expires=${expires.toUTCString()}`;
    } else if (expires) {
        updatedCookie += `;max-age=${expires}`;
    }

    document.cookie = updatedCookie;
}

export function getCookie(name: string) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([.$?*|{}()[\]\\/+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

export function deleteCookie(name: string) {
    setCookie(name, "", -1);
}