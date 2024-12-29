/*
 * Apply Bootstrap dark mode.
 */
if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
    document.documentElement.dataset.bsTheme = "dark";
}

/*
 * Perform the search function when typing in the text box.
 */
var searchBox = document.getElementById("searchBox");
searchBox?.addEventListener('change', function (event) {
    searchFunction(event.target.value);
});

/*
 * Scroll to the next result when loading additional results.
 */
var scrollTo = document.getElementById("scrollTo");
document.addEventListener('DOMContentLoaded', function () {
    scrollTo?.scrollIntoView();
});

/*
 * Episode Listing checkbox to show/hide non-story content.
 */
var listExcluded = document.getElementById("listExcluded");
listExcluded?.addEventListener('change', function (event) {
    var excludedElements = document.getElementsByClassName("excluded");
    if (listExcluded.checked) {
        for (let i = 0; i < excludedElements.length; i++) {
            excludedElements[i].classList.add("d-table-row");
        }
    } else {
        for (let i = 0; i < excludedElements.length; i++) {
            excludedElements[i].classList.remove("d-table-row");
        }
    }
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