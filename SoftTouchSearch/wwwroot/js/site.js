var searchBox = document.getElementById("searchBox");
searchBox?.addEventListener('change', function (event) {
    console.log(event);
    searchFunction(event.target.value);
});

/**
 * Main search function. Appends the search text as a query string, which the server will then process.
 * @param {string} text The text to search for.
 */
function searchFunction(text) {
    let query = encodeURIComponent(text);

    let currentUrl = window.location.href;
    let urlParts = currentUrl.split('?');
    currentUrl = urlParts[0];
    currentUrl += `?q=${query}`;

    window.location.href = currentUrl;
}