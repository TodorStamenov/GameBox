function containsKey(key) {
    return !!localStorage.getItem(key);
}

function getItem(key) {
    return localStorage.getItem(key);
}

function setItem(key, value) {
    localStorage.setItem(key, value);
}

function clear() {
    localStorage.clear();
}